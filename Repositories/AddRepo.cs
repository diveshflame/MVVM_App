using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WpfApp4.Models;
using System.Reflection.PortableExecutable;
using Npgsql;

namespace WpfApp4.Repositories
{
  
    public class AddRepo : RepositoryBase,IAddDocRepo
    {
        public List<string>get()
        {
            List<string> list2 = new List<string>();

            using (var connection = GetConnection()) // You need to replace GetConnection() with your actual connection creation logic
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT Doctor_Name from Doctor_Table";

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Add the value from the "Doctor_Name" column to the j list
                        list2.Add(reader.GetString(0)); // Assuming the column is of string type
                    }
                }
            }
            return list2;
        }


    }
}
