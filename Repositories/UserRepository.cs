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
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;

namespace MVVM_App.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public bool IsUserSuperUser(NetworkCredential credential)
        {
            // Query the database to check if the user is a superuser
            // Return true if superuser, false otherwise
            bool validUser=false;
            using (var connection = GetConnection())
            {
                using (var command = new NpgsqlCommand())
                {
                    connection.Open();
                    //To check if user is Admin
                    command.Connection = connection;
                    command.CommandText = "select count(*) from userDetails where UserName=@UserName AND Password=@Password AND super_user=1";
                    int count = 0;
                    command.Parameters.AddWithValue("@UserName", credential.UserName);
                    command.Parameters.AddWithValue("@Password", credential.Password);
                    // int count = Convert.ToInt32(command.ExecuteScalar());
                    using (command)
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                count = reader.GetInt32(0);
                            }
                        }
                        if (count > 0)
                        {
                            validUser = true;
                        }
                    }

                }
            }
            return validUser;
        }
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
                command.CommandText = "select * from userdetails where username=@username and password=@password and super_user=0";
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
