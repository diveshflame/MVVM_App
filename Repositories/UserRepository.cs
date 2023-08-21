using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MVVM_App.Models;

using Npgsql;
using NpgsqlTypes;

namespace MVVM_App.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetConnection())
                using(var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from userdetails where username=@username and password=@password";
                command.Parameters.Add("@username", NpgsqlDbType.Varchar).Value=credential.UserName;
                command.Parameters.Add("@password", NpgsqlDbType.Varchar).Value = credential.Password;

                validUser = command.ExecuteScalar() == null ? false : true;
            }
            return validUser;
        }

        public UserModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public UserModel GetByUsername(string username)
        {
            UserModel validUser = null;
            using (var connection = GetConnection())
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from userdetails where username=@username ";

                command.Parameters.Add("@username", NpgsqlDbType.Varchar).Value = username;

                using (var reader = command.ExecuteReader()) 
                {
                if(reader.Read())
                    {
                        validUser = new UserModel()
                        {
                            UserName = reader[0].ToString(),
                            Password = reader[1].ToString(),

                        };
                    }
                }
              
            }
            return validUser;
        }
    }
}
