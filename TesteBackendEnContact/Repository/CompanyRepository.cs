using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook.Company;
using TesteBackendEnContact.Core.Interface.ContactBook;
using TesteBackendEnContact.Core.Interface.ContactBook.Company;
using TesteBackendEnContact.Database;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.ViewModels;

namespace TesteBackendEnContact.Repository
{
    public class CompanyRepository : IRepository<ICompany>
    {
        private readonly DatabaseConfig databaseConfig;
        private readonly IRepository<IContactBook> _contactBookRepository;

        public CompanyRepository(DatabaseConfig databaseConfig, IRepository<IContactBook> contactBookRepository)
        {
            this.databaseConfig = databaseConfig;
            _contactBookRepository = contactBookRepository;
        }

        public async Task<ResultViewModel<ICompany>> SaveAsync(ICompany company)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var contackBook = await _contactBookRepository.GetAsync(company.ContactBookId);
            if (contackBook.Data == null) return new ResultViewModel<ICompany>("Não existe agenda com esse id");

            var dao = new CompanyDao(company);

            if (dao.Id == 0)
                dao.Id = await connection.InsertAsync(dao);
            else
                await connection.UpdateAsync(dao);

            return new ResultViewModel<ICompany>(dao.Export());
        }

        public async Task<ResultViewModel<ICompany>> DeleteAsync(int id)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);
            await connection.OpenAsync();
            using var transaction = await connection.BeginTransactionAsync();

            var company = await GetAsync(id);
            if (company.Data == null) return new ResultViewModel<ICompany>("Não existe compania com esse id");
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM Company WHERE Id = @id;");
            sql.AppendLine("UPDATE Contact SET CompanyId = null WHERE CompanyId = @id;");

            await connection.ExecuteAsync(sql.ToString(), new { id }, transaction);
            await transaction.CommitAsync();

            return new ResultViewModel<ICompany>();
        }

        public async Task<ResultViewModel<IEnumerable<ICompany>>> GetAllAsync()
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var query = "SELECT * FROM Company";
            var result = await connection.QueryAsync<CompanyDao>(query);
            var list = result.ToList();
            return new ResultViewModel<IEnumerable<ICompany>>(result.ToList());
        }

        public async Task<ResultViewModel<ICompany>> GetAsync(int id)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var company = await connection.GetAsync<CompanyDao>(id);

            if (company == null) return new ResultViewModel<ICompany>("Não existe compania com esse Id");

            return new ResultViewModel<ICompany>(company);
        }

        public Task<ResultViewModel<ICompany>> UpdateAsync(int id, ICompany entity)
        {
            throw new System.NotImplementedException();
        }
    }

    [Table("Company")]
    public class CompanyDao : ICompany
    {
        [Key]
        public int Id { get; set; }
        public int ContactBookId { get; set; }
        public string Name { get; set; }

        public CompanyDao()
        {
        }

        public CompanyDao(ICompany company)
        {
            Id = company.Id;
            ContactBookId = company.ContactBookId;
            Name = company.Name;
        }

        public ICompany Export() => new Company(Id, ContactBookId, Name);
    }
}
