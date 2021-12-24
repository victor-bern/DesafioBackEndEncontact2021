using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.User;
using TesteBackendEnContact.Database;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.ViewModels;

namespace TesteBackendEnContact.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly DatabaseConfig _databaseConfig;
        public UserRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<ResultViewModel<IEnumerable<User>>> GetAllAsync()
        {
            using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
            var query = "SELECT * FROM User";

            var result = await connection.QueryAsync<User>(query);

            return new ResultViewModel<IEnumerable<User>>(result.ToList());
        }



        public async Task<ResultViewModel<User>> GetAsync(int id)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

                var user = await connection.GetAsync<User>(id);

                if (user == null) return new ResultViewModel<User>("Usuário não encontrado");

                return new ResultViewModel<User>(user);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<User>("Houve um erro ao tentar recuperar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<User>("Internal Server Error");
            }

        }


        public async Task<ResultViewModel<User>> SaveAsync(User entity)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
                var user = new User(entity);
                user.Id = await connection.InsertAsync(user);

                return new ResultViewModel<User>(user);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<User>("Já existe um usuário com este email");
            }
            catch (Exception)
            {
                return new ResultViewModel<User>("Internal Server Error");
            }

        }

        public async Task<ResultViewModel<User>> UpdateAsync(int id, User entity)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
                var user = await connection.GetAsync<User>(id);
                if (user == null) return new ResultViewModel<User>("Contato não encontrado");
                user.Email = string.IsNullOrEmpty(entity.Email) ? user.Email : entity.Email;
                user.Password = string.IsNullOrEmpty(entity.Password) ? user.Password : entity.Password;

                await connection.UpdateAsync(user);

                return new ResultViewModel<User>(user);
            }
            catch (SqliteException)
            {
                return new ResultViewModel<User>("Houve um erro ao tentar atualizar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<User>("Internal Server Error");
            }
        }

        public async Task<ResultViewModel<User>> DeleteAsync(int id)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
                var user = await connection.GetAsync<User>(id);
                if (user == null) return new ResultViewModel<User>("Contato não encontrado");

                await connection.DeleteAsync(user);

                return new ResultViewModel<User>();
            }
            catch (SqliteException)
            {
                return new ResultViewModel<User>("Houve um erro ao tentar deletar os dados");
            }
            catch (Exception)
            {
                return new ResultViewModel<User>("Internal Server Error");
            }
        }

    }
}
