using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MVVM_App.Models;
using System.Reflection.PortableExecutable;
using Npgsql;
using System.Windows;

namespace MVVM_App.Repositories
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

            public List<string>getco()
        {
            List<string> list2 = new List<string>();

            using (var connection = GetConnection()) // You need to replace GetConnection() with your actual connection creation logic
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT consultant_desc from consultant_type";

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




        //Add Consultation function
        public void add(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Please enter Valid Consultation Type!!", "Error Message");
            }
            else
            {
                int count = 0;
                using (var connection = GetConnection()) // You need to replace GetConnection() with your actual connection creation logic
                using (var command = new NpgsqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "Select COUNT(*) from Consultant_Type where consultant_Desc =@d";
                    command.Parameters.Add(new NpgsqlParameter("@d", id));
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            count=reader.GetInt32(0);   
                        }
                    }
                            if (count>0)
                    {
                        MessageBox.Show("Consultant Type Already Exists", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {
                       

                        using (NpgsqlConnection conn = GetConnection())
                        {
                            string insert = "insert into Consultant_Type(consultant_desc) values(@Consult_Desc)";
                            NpgsqlCommand cmd = new NpgsqlCommand(insert, conn);
                            cmd.Parameters.Add(new NpgsqlParameter("@Consult_Desc", id));
                           
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Successfully Added !!", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                            conn.Close();
                        }
                    }

                }
            }

        }


        //Link with demotime

        public List<TimeSpan> startT()
        {
            List<TimeSpan> list2 = new List<TimeSpan>();

            using (var connection = GetConnection()) // You need to replace GetConnection() with your actual connection creation logic
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT demostarttime from demo_time";

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Add the value from the "Doctor_Name" column to the j list
                        list2.Add(reader.GetTimeSpan(0)); // Assuming the column is of string type
                    }
                }
            }
            return list2;
        }

        public List<TimeSpan> EndT()
        {
            List<TimeSpan> list2 = new List<TimeSpan>();

            using (var connection = GetConnection()) // You need to replace GetConnection() with your actual connection creation logic
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT demoendtime from demo_time";

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Add the value from the "Doctor_Name" column to the j list
                        list2.Add(reader.GetTimeSpan(0)); // Assuming the column is of string type
                    }
                }
            }
            return list2;
        }



    }
}
