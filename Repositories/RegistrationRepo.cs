using MVVM_App.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_App.Models;

namespace MVVM_App.Repositories
{
    public class RegistrationRepo : RepositoryBase
    {
        public void InsertUser(UserDetails userDetails)
        {
            try
            {
                using (var connection = GetConnection())
                //GetConnection() is function from RepositoryBase which creates connection with postgres database
                {
                    connection.Open();

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "INSERT INTO userdetails (username, name, age, gender, email, phone_number, password) " +
                                          "VALUES (@username, @name, @age, @gender, @email, @phone_number, @password)";

                        cmd.Parameters.AddWithValue("username", userDetails.Username);
                        cmd.Parameters.AddWithValue("name", userDetails.FullName);
                        cmd.Parameters.AddWithValue("age", userDetails.Age);
                        cmd.Parameters.AddWithValue("gender", userDetails.Gender);
                        cmd.Parameters.AddWithValue("email", userDetails.Email);
                        cmd.Parameters.AddWithValue("phone_number", userDetails.PhoneNumber);
                        cmd.Parameters.AddWithValue("password", userDetails.Password);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred :" + ex.Message);
            }
        }
    }
    






}
