using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
using System;
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
        private readonly DatabaseConfig _databaseConfig;
        private readonly IRepository<IContactBook> _contactBookRepository;

        public CompanyRepository(DatabaseConfig databaseConfig, IRepository<IContactBook> contactBookRepository)
        {
            _databaseConfig = databaseConfig;
            _contactBookRepository = contactBookRepository;
        }

        public async Task<ResultViewModel<ICompany>> SaveAsync(ICompany entity)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

                var contackBook = await _contactBookRepository.GetAsync(entity.ContactBookId);
                if (contackBook.Data == null) return new ResultViewModel<ICompany>("Nenhuma agenda encontrada não foi possivel salvar");

                var company = new Company(entity);

                await connection.InsertAsync(company);

                return new ResultViewModel<ICompany>(company);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<ICompany>("Houve um erro ao tentar salvar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<ICompany>("Internal Server Error");
            }

        }

        public async Task<ResultViewModel<IEnumerable<ICompany>>> GetAllAsync()
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

                var query = "SELECT * FROM Company";
                var result = await connection.QueryAsync<Company>(query);
                var list = result.ToList();
                return new ResultViewModel<IEnumerable<ICompany>>(result.ToList());
            }
            catch (SqliteException)
            {
                return new ResultViewModel<IEnumerable<ICompany>>("Houve um erro ao tentar recuperar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<IEnumerable<ICompany>>("Internal Server Error");
            }

        }

        public async Task<ResultViewModel<ICompany>> GetAsync(int id)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

                var company = await connection.GetAsync<Company>(id);

                if (company == null) return new ResultViewModel<ICompany>("Não foi encontrada nenhuma empresa");

                return new ResultViewModel<ICompany>(company);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<ICompany>("Houve um erro ao tentar recuperar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<ICompany>("Internal Server Error");
            }


        }

        public async Task<ResultViewModel<ICompany>> UpdateAsync(int id, ICompany entity)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
                var company = await connection.GetAsync<Company>(id);
                if (company == null) return new ResultViewModel<ICompany>("Empresa não encontrada");
                company.Name = string.IsNullOrEmpty(entity.Name) ? company.Name : entity.Name;

                await connection.UpdateAsync(company);

                return new ResultViewModel<ICompany>(company);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<ICompany>("Houve um erro ao tentar atualizar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<ICompany>("Internal Server Error");
            }
        }

        public async Task<ResultViewModel<ICompany>> DeleteAsync(int id)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
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
            catch (SqliteException)
            {
                return new ResultViewModel<ICompany>("Houve um erro ao tentar deletar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<ICompany>("Internal Server Error");
            }
        }
    }

}
