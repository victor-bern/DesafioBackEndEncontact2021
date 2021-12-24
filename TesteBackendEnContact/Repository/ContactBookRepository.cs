using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook;
using TesteBackendEnContact.Database;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.ViewModels;

namespace TesteBackendEnContact.Repository
{
    public class ContactBookRepository : IContactBookRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public ContactBookRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }




        public async Task<ResultViewModel<IEnumerable<ContactBook>>> GetAllAsync()
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

                var query = "SELECT * FROM ContactBook";
                var result = await connection.QueryAsync<ContactBook>(query);

                return new ResultViewModel<IEnumerable<ContactBook>>(result.ToList());

            }
            catch (SqliteException)
            {
                return new ResultViewModel<IEnumerable<ContactBook>>("Houve um erro ao tentar recuperar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<IEnumerable<ContactBook>>("Erro interno");
            }

        }
        public async Task<ResultViewModel<ContactBook>> GetAsync(int id)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

                var contactBook = await connection.GetAsync<ContactBook>(id);

                if (contactBook == null) return new ResultViewModel<ContactBook>("Agenda não encontrada");

                return new ResultViewModel<ContactBook>(contactBook);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<ContactBook>("Houve um erro ao tentar recuperar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<ContactBook>("Internal Server Error");
            }
        }
        public async Task<ResultViewModel<ContactBook>> GetByNameAsync(string name)
        {
            try
            {
                name = name.Trim().ToLower();
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

                var query = "SELECT * FROM ContactBook WHERE LOWER(Name) = @name";
                var contactBook = await connection.QueryFirstOrDefaultAsync<ContactBook>(query, new { name });

                if (contactBook == null) return new ResultViewModel<ContactBook>("Não foi encontrada nenhuma agenda");

                return new ResultViewModel<ContactBook>(contactBook);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<ContactBook>("Houve um erro ao tentar recuperar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<ContactBook>("Internal Server Error");
            }
        }
        public async Task<ResultViewModel<ContactBook>> SaveAsync(ContactBook entity)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
                var contactBook = new ContactBook(entity);

                contactBook.Id = await connection.InsertAsync(contactBook);

                return new ResultViewModel<ContactBook>(contactBook);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<ContactBook>("Houve um erro ao tentar salvar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<ContactBook>("Internal Server Error");
            }

        }
        public async Task<ResultViewModel<ContactBook>> UpdateAsync(int id, ContactBook entity)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
                var contactBook = await connection.GetAsync<ContactBook>(id);
                if (contactBook == null) return new ResultViewModel<ContactBook>("Agenda não encontrada");
                contactBook.Name = string.IsNullOrEmpty(entity.Name) ? contactBook.Name : entity.Name;

                await connection.UpdateAsync(contactBook);

                return new ResultViewModel<ContactBook>(contactBook);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<ContactBook>("Houve um erro ao tentar atualizar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<ContactBook>("Internal Server Error");
            }
        }

        public async Task<ResultViewModel<ContactBook>> DeleteAsync(int id)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

                var contactBook = await GetAsync(id);
                if (contactBook.Data == null) return new ResultViewModel<ContactBook>("Não existe agenda com esse id");
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("DELETE FROM ContactBook WHERE Id = @ContactBookId;");
                stringBuilder.AppendLine("DELETE FROM Company WHERE ContactBookId = @ContactBookId;");
                stringBuilder.AppendLine("DELETE FROM Contact WHERE ContactBookId = @ContactBookId;");
                await connection.ExecuteAsync(stringBuilder.ToString(), new { ContactBookId = contactBook.Data.Id }); ;

                return new ResultViewModel<ContactBook>();
            }
            catch (SqliteException)
            {
                return new ResultViewModel<ContactBook>("Houve um erro ao tentar deletar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<ContactBook>("Internal Server Error");
            }

        }


        public async Task TruncateTables()
        {
            using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("TRUNCATE TABLE [Company];");
            stringBuilder.AppendLine("TRUNCATE TABLE [Contact];");
            stringBuilder.AppendLine("TRUNCATE TABLE [ContactBook];");

            var query = connection.ExecuteAsync(stringBuilder.ToString());

        }
    }
}
