using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
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
        private readonly DatabaseConfig databaseConfig;

        public ContactBookRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }


        public async Task<ResultViewModel<IContactBook>> SaveAsync(IContactBook contactBook)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);
            var dao = new ContactBookDao(contactBook);

            dao.Id = await connection.InsertAsync(dao);

            return new ResultViewModel<IContactBook>(dao.Export());
        }


        public async Task<ResultViewModel<IContactBook>> DeleteAsync(int id)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var contactBook = await GetAsync(id);
            if (contactBook.Data == null) return new ResultViewModel<IContactBook>("Não existe agenda com esse id");
            var sql = "DELETE FROM ContactBook WHERE Id = @id";

            await connection.ExecuteAsync(sql, new
            {
                id
            });

            return new ResultViewModel<IContactBook>();
        }


        public async Task<ResultViewModel<IEnumerable<IContactBook>>> GetAllAsync()
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var query = "SELECT * FROM ContactBook";
            var result = await connection.QueryAsync<ContactBookDao>(query);

            var returnList = new List<IContactBook>();

            return new ResultViewModel<IEnumerable<IContactBook>>(result.ToList());
        }

        public async Task<ResultViewModel<IContactBook>> GetAsync(int id)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var contactBook = await connection.GetAsync<ContactBookDao>(id);

            if (contactBook == null) return new ResultViewModel<IContactBook>("Não existe agenda com esse id");

            return new ResultViewModel<IContactBook>(contactBook);
        }
    }

    [Table("ContactBook")]
    public class ContactBookDao : IContactBook
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ContactBookDao()
        {
        }

        public ContactBookDao(IContactBook contactBook)
        {
            Id = contactBook.Id;
            Name = contactBook.Name;
        }

        public IContactBook Export() => new ContactBook(Id, Name);
    }
}
