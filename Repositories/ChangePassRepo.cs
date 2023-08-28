using MVVM_App.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_App.Repositories
{
    public class ChangePassRepo : RepositoryBase
    {
        public void ChangeUserPass(UserDetails userDetails)
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
                        cmd.CommandText = "update userDetails set password=@password where username=@username";
                        cmd.Parameters.Add(new NpgsqlParameter("@username", userDetails.ChangeUsername));
                        cmd.Parameters.Add(new NpgsqlParameter("@password", userDetails.ChangePassword));
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
