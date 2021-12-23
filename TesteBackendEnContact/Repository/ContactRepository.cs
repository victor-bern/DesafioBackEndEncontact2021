using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook.Contact;
using TesteBackendEnContact.Core.Interface.ContactBook.Contact;
using TesteBackendEnContact.Database;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.ViewModels;

namespace TesteBackendEnContact.Repository
{
    public class ContactRepository : IContactRepository
    {

        private readonly DatabaseConfig _databaseConfig;
        public ContactRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<ResultViewModel<IEnumerable<IContact>>> GetAllAsync()
        {
            using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
            var query = "SELECT * FROM Contact";

            var result = await connection.QueryAsync<Contact>(query);

            return new ResultViewModel<IEnumerable<IContact>>(result.ToList());
        }



        public async Task<ResultViewModel<IContact>> GetAsync(int id)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

                var contact = await connection.GetAsync<Contact>(id);

                if (contact == null) return new ResultViewModel<IContact>("Não existe contato com esse Id");

                return new ResultViewModel<IContact>(contact);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<IContact>("Houve um erro ao tentar recuperar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<IContact>("Internal Server Error");
            }

        }

        public async Task<ResultViewModel<IContact>> GetByParamAsync(string param, string value)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
                param = param.ToLower();
                param = char.ToUpper(param[0]) + param[1..].Replace(" ", "");
                var query = "SELECT * FROM Contact WHERE " + param + " = @value";
                var contact = await connection.QuerySingleAsync<Contact>(query, new { value });

                if (contact == null) return new ResultViewModel<IContact>($"Não existe contato com esse {param}");


                return new ResultViewModel<IContact>(contact);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<IContact>("Houve um erro ao tentar recuperar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<IContact>("Internal Server Error");
            }

        }


        public async Task<ResultViewModel<IContact>> SaveAsync(IContact entity)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
                var contact = new Contact(entity);
                contact.Id = await connection.InsertAsync(contact);

                return new ResultViewModel<IContact>(contact);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<IContact>("Houve um erro ao tentar salvar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<IContact>("Internal Server Error");
            }

        }



        public Task<ResultViewModel<IContact>> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ResultViewModel<IContact>> UpdateAsync(int id, IContact entity)
        {
            throw new NotImplementedException();
        }
    }
}
