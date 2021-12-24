using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook.Company;
using TesteBackendEnContact.Core.Domain.ContactBook.Contact;
using TesteBackendEnContact.Database;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.ViewModels;

namespace TesteBackendEnContact.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DatabaseConfig _databaseConfig;
        private readonly IContactBookRepository _contactBookRepository;

        public CompanyRepository(DatabaseConfig databaseConfig, IContactBookRepository contactBookRepository)
        {
            _databaseConfig = databaseConfig;
            _contactBookRepository = contactBookRepository;
        }

        public async Task<ResultViewModel<Company>> SaveAsync(Company entity)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

                var contackBook = await _contactBookRepository.GetAsync(entity.ContactBookId);
                if (contackBook.Data == null) return new ResultViewModel<Company>("Nenhuma agenda encontrada não foi possivel salvar");

                var company = new Company(entity);

                await connection.InsertAsync(company);

                return new ResultViewModel<Company>(company);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<Company>("Houve um erro ao tentar salvar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<Company>("Internal Server Error");
            }

        }

        public virtual async Task<ResultViewModel<IEnumerable<CompanyListViewModel>>> GetAllAsync()
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
                var companies = new List<CompanyListViewModel>();
                var query = @"
                        SELECT 
                            [Company].[Id],
                            [Company].[Name],
                            [Contact].[Id] AS ContactId,
                            [Contact].[Name],
                            [Contact].[Phone],
                            [Contact].[Address],
                            [Contact].[Email],
                            [Contact].[CompanyId],
                            [Contact].[ContactBookId]
                        FROM [Company]
                        LEFT join [Contact] ON [Contact].[CompanyId] = [Company].[Id]";

                var result = await connection.QueryAsync<CompanyListViewModel, Contact, CompanyListViewModel>(query, (company, contact) =>
                {
                    if (contact.Name == null)
                    {
                        company.Contacts = null;
                        return company;
                    }
                    company.Contacts.Add(contact);
                    return company;
                }, splitOn: "ContactId");

                return new ResultViewModel<IEnumerable<CompanyListViewModel>>(result);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<IEnumerable<CompanyListViewModel>>("Houve um erro ao tentar recuperar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<IEnumerable<CompanyListViewModel>>("Internal Server Error");
            }

        }

        public async Task<ResultViewModel<Company>> GetAsync(int id)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

                var company = await connection.GetAsync<Company>(id);

                if (company == null) return new ResultViewModel<Company>("Não foi encontrada nenhuma empresa");

                return new ResultViewModel<Company>(company);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<Company>("Houve um erro ao tentar recuperar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<Company>("Internal Server Error");
            }


        }

        public async Task<ResultViewModel<Company>> GetCompanyByNameAsync(string name)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

                var query = "SELECT * FROM Company WHERE Name iLIKE %@name%";
                var company = await connection.QueryFirstOrDefaultAsync<Company>(query);

                if (company == null) return new ResultViewModel<Company>("Não foi encontrada nenhuma empresa");

                return new ResultViewModel<Company>(company);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<Company>("Houve um erro ao tentar recuperar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<Company>("Internal Server Error");
            }


        }
        public async Task<ResultViewModel<Company>> UpdateAsync(int id, Company entity)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
                var company = await connection.GetAsync<Company>(id);
                if (company == null) return new ResultViewModel<Company>("Empresa não encontrada");
                company.Name = string.IsNullOrEmpty(entity.Name) ? company.Name : entity.Name;

                await connection.UpdateAsync(company);

                return new ResultViewModel<Company>(company);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<Company>("Houve um erro ao tentar atualizar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<Company>("Internal Server Error");
            }
        }

        public async Task<ResultViewModel<Company>> DeleteAsync(int id)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
                await connection.OpenAsync();
                using var transaction = await connection.BeginTransactionAsync();

                var company = await GetAsync(id);
                if (company.Data == null) return new ResultViewModel<Company>("Não existe compania com esse id");
                var sql = new StringBuilder();
                sql.AppendLine("DELETE FROM Company WHERE Id = @id;");
                sql.AppendLine("UPDATE Contact SET CompanyId = null WHERE CompanyId = @id;");

                await connection.ExecuteAsync(sql.ToString(), new { id }, transaction);
                await transaction.CommitAsync();

                return new ResultViewModel<Company>();
            }
            catch (SqliteException)
            {
                return new ResultViewModel<Company>("Houve um erro ao tentar deletar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<Company>("Internal Server Error");
            }
        }

        public async Task<ResultViewModel<Company>> GetByNameAsync(string name)
        {
            try
            {
                name = name.Trim().ToLower();
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

                var query = "SELECT * FROM Company WHERE LOWER(Name) = @name";
                var company = await connection.QueryFirstOrDefaultAsync<Company>(query, new { name });

                if (company == null) return new ResultViewModel<Company>("Não foi encontrada nenhuma empresa");

                return new ResultViewModel<Company>(company);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<Company>("Houve um erro ao tentar recuperar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<Company>("Internal Server Error");
            }
        }
    }

}
