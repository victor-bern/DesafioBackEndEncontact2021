using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook.Contact;
using TesteBackendEnContact.Database;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.Services.Interface;
using TesteBackendEnContact.ViewModels;

namespace TesteBackendEnContact.Repository
{
    public class ContactRepository : IContactRepository
    {

        private readonly DatabaseConfig _databaseConfig;
        private readonly IContactService _contactService;
        public ContactRepository(DatabaseConfig databaseConfig, IContactService contactService)
        {
            _databaseConfig = databaseConfig;
            _contactService = contactService;
        }

        public async Task<ResultViewModel<IEnumerable<Contact>>> GetAllAsync()
        {
            using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
            var query = "SELECT * FROM Contact";

            var result = await connection.QueryAsync<Contact>(query);

            return new ResultViewModel<IEnumerable<Contact>>(result.ToList());
        }



        public async Task<ResultViewModel<Contact>> GetAsync(int id)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

                var contact = await connection.GetAsync<Contact>(id);

                if (contact == null) return new ResultViewModel<Contact>("Não existe contato com esse Id");

                return new ResultViewModel<Contact>(contact);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<Contact>("Houve um erro ao tentar recuperar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<Contact>("Internal Server Error");
            }

        }

        public async Task<ResultViewModel<Contact>> GetByParamAsync(string param, string value)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
                param = param.ToLower();
                param = char.ToUpper(param[0]) + param[1..].Replace(" ", "");
                var query = "SELECT * FROM Contact WHERE " + param + " = @value";
                var contact = await connection.QueryFirstOrDefaultAsync<Contact>(query, new { value });

                if (contact == null) return new ResultViewModel<Contact>($"Não existe contato com esse {param}");


                return new ResultViewModel<Contact>(contact);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<Contact>("Houve um erro ao tentar recuperar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<Contact>("Internal Server Error");
            }

        }


        public async Task<ResultViewModel<Contact>> SaveAsync(Contact entity)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
                var contact = new Contact(entity);
                contact.Id = await connection.InsertAsync(contact);

                return new ResultViewModel<Contact>(contact);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<Contact>("Houve um erro ao tentar salvar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<Contact>("Internal Server Error");
            }

        }

        public async Task<ResultViewModel<Contact>> UpdateAsync(int id, Contact entity)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
                var contact = await connection.GetAsync<Contact>(id);
                if (contact == null) return new ResultViewModel<Contact>("Contato não encontrado");
                contact.Name = string.IsNullOrEmpty(entity.Name) ? contact.Name : entity.Name;
                contact.Phone = string.IsNullOrEmpty(entity.Phone) ? contact.Phone : entity.Phone;
                contact.Email = string.IsNullOrEmpty(entity.Email) ? contact.Email : entity.Email;
                contact.Address = string.IsNullOrEmpty(entity.Address) ? contact.Address : entity.Address;

                await connection.UpdateAsync(contact);

                return new ResultViewModel<Contact>(contact);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<Contact>("Houve um erro ao tentar atualizar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<Contact>("Internal Server Error");
            }
        }

        public async Task<ResultViewModel<Contact>> DeleteAsync(int id)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
                var contact = await connection.GetAsync<Contact>(id);
                if (contact == null) return new ResultViewModel<Contact>("Contato não encontrado");

                await connection.DeleteAsync(contact);

                return new ResultViewModel<Contact>();
            }
            catch (SqliteException)
            {
                return new ResultViewModel<Contact>("Houve um erro ao tentar deletar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<Contact>("Internal Server Error");
            }
        }

        public async Task<ResultViewModel<IEnumerable<Contact>>> UploadContactsByFile(IFormFile file)
        {
            using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
            var contacts = await _contactService.ExtractContacts(file);

            foreach (var contact in contacts)
            {
                await connection.InsertAsync(contact);
            }


            return new ResultViewModel<IEnumerable<Contact>>(contacts);
        }

        public async Task<ResultViewModel<Contact>> GetByNameAsync(string name)
        {
            try
            {
                name = name.Trim().ToLower();
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

                var query = "SELECT * FROM Contact WHERE LOWER(Name) = @name";
                var contact = await connection.QueryFirstOrDefaultAsync<Contact>(query, new { name });

                if (contact == null) return new ResultViewModel<Contact>("Não foi encontrado nenhum contato");

                return new ResultViewModel<Contact>(contact);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<Contact>("Houve um erro ao tentar recuperar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<Contact>("Internal Server Error");
            }
        }
    }
}
