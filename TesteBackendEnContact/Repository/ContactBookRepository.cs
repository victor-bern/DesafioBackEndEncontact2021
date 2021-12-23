using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook;
using TesteBackendEnContact.Core.Interface.ContactBook;
using TesteBackendEnContact.Database;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.ViewModels;

namespace TesteBackendEnContact.Repository
{
    public class ContactBookRepository : IRepository<IContactBook>
    {
        private readonly DatabaseConfig _databaseConfig;

        public ContactBookRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }


        public async Task<ResultViewModel<IContactBook>> SaveAsync(IContactBook entity)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
                var contactBook = new ContactBook(entity);

                contactBook.Id = await connection.InsertAsync(contactBook);

                return new ResultViewModel<IContactBook>(contactBook);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<IContactBook>("Houve um erro ao tentar recuperar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<IContactBook>("Internal Server Error");
            }

        }


        public async Task<ResultViewModel<IEnumerable<IContactBook>>> GetAllAsync()
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

                var query = "SELECT * FROM ContactBook";
                var result = await connection.QueryAsync<ContactBook>(query);

                return new ResultViewModel<IEnumerable<IContactBook>>(result.ToList());

            }
            catch (SqliteException)
            {
                return new ResultViewModel<IEnumerable<IContactBook>>("Houve um erro ao tentar recuperar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<IEnumerable<IContactBook>>("Erro interno");
            }

        }

        public async Task<ResultViewModel<IContactBook>> GetAsync(int id)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

                var contactBook = await connection.GetAsync<ContactBook>(id);

                if (contactBook == null) return new ResultViewModel<IContactBook>("Agenda não encontrada");

                return new ResultViewModel<IContactBook>(contactBook);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<IContactBook>("Houve um erro ao tentar recuperar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<IContactBook>("Internal Server Error");
            }
        }

        public async Task<ResultViewModel<IContactBook>> UpdateAsync(int id, IContactBook entity)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
                var contactBook = await connection.GetAsync<ContactBook>(id);
                if (contactBook == null) return new ResultViewModel<IContactBook>("Agenda não encontrada");
                contactBook.Name = string.IsNullOrEmpty(entity.Name) ? contactBook.Name : entity.Name;

                await connection.UpdateAsync(contactBook);

                return new ResultViewModel<IContactBook>(contactBook);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<IContactBook>("Houve um erro ao tentar atualizar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<IContactBook>("Internal Server Error");
            }
        }

        public async Task<ResultViewModel<IContactBook>> DeleteAsync(int id)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

                var contactBook = await GetAsync(id);
                if (contactBook.Data == null) return new ResultViewModel<IContactBook>("Não existe agenda com esse id");
                await connection.DeleteAsync(contactBook);

                return new ResultViewModel<IContactBook>();
            }
            catch (SqliteException)
            {
                return new ResultViewModel<IContactBook>("Houve um erro ao tentar deletar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<IContactBook>("Internal Server Error");
            }

        }
    }
}
