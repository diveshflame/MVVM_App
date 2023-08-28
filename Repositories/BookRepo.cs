using MVVM_App.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace MVVM_App.Repositories
{
    public class BookRepo : RepositoryBase
    {
        public List<string> slotlist = new List<string>();
       public List<string> slotlist2 = new List<string>();
        public List<string> bookgetco()
        {
            List<string> list2 = new List<string>();

            using (var connection = GetConnection()) 
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT consultant_desc from consultant_type";

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        list2.Add(reader.GetString(0)); 
                    }
                }
            }
            return list2;
        }

        public List<string> bookgetDoc(string s)
        {
            int a = 0;
            using (var connection = GetConnection()) 
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT consultant_id from consultant_Type where @c = consultant_desc";
                command.Parameters.Add(new NpgsqlParameter("@c", s));
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        a = reader.GetInt32(0);

                        
                    }
                }
            }
           List<string> list = new List<string>();  
            using (var connection = GetConnection())
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT doctor_Name from doctor_table where @d=consultant_id";
                command.Parameters.Add(new NpgsqlParameter("@d", a));
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        list.Add(reader.GetString(0));  

                        
                    }
                }
            }
            return list;
        }

        public void BookGetTime(string doc, DateTime selectedDate)
        {
            string c = "";
            int id = 0;
            DateTime ne = DateTime.Now;
         
            int b = 0;
            using (var connection = GetConnection()) 
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT doctor_Id from doctor_table where @d=Doctor_Name";
                command.Parameters.Add(new NpgsqlParameter("@d", doc));
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                       
                        b = reader.GetInt32(0);

                      
                    }
                }
            }
            using (var connection = GetConnection()) 
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT available_starttime, available_endtime FROM doctor_availability WHERE DATE(doctor_availability.available_starttime) = @j AND DATE(doctor_availability.available_endtime) = @j AND doctor_availability.doctor_id = @u EXCEPT SELECT available_starttime, available_endtime FROM booking_table, doctor_availability WHERE booking_table.StartTime = available_starttime AND booking_table.doctor_id = @u AND booking_table.EndTime = available_endtime AND Deleted_TimeStamp IS NULL ORDER BY available_starttime, available_endtime;";
                command.Parameters.Add(new NpgsqlParameter("@j", selectedDate));
                command.Parameters.Add(new NpgsqlParameter("@u", b));
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                       
                        TimeSpan timeSpan1= reader.GetDateTime(0).TimeOfDay;
                        TimeSpan timeSpan2= reader.GetDateTime(1).TimeOfDay;
                      
                        string hhmmFormat1 = $"{timeSpan1.Hours:D2}:{timeSpan1.Minutes:D2}";
                        string hhmmFormat2 = $"{timeSpan2.Hours:D2}:{timeSpan2.Minutes:D2}";
                       
                        slotlist.Add(hhmmFormat1);
                        slotlist2.Add(hhmmFormat2); 

                       
                    }
                }
            }

        }

        public void AddBook(string selectedDep, DateTime selectedDate, string doc, DateTime d1, DateTime d2)
        {
            int a = 0;
            using (var connection = GetConnection()) 
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT consultant_id from consultant_Type where @c = consultant_desc";
                command.Parameters.Add(new NpgsqlParameter("@c", selectedDep));
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                       
                        a = reader.GetInt32(0);

                       
                    }
                }
            }
            int b = 0;
            using (var connection = GetConnection()) 
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT doctor_Id from doctor_table where @d=Doctor_Name";
                command.Parameters.Add(new NpgsqlParameter("@d", doc));
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        b = reader.GetInt32(0);

                       
                    }
                }
            }
            int k = 0;
            using (var connection = GetConnection()) // You need to replace GetConnection() with your actual connection creation logic
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT userid from userdetails where active_session = 1";
              
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                       
                        k = reader.GetInt32(0);

                        // Assuming the column is of string type
                    }
                }
            }
            DateTime dc1 = DateTime.Now;
            DateTime dc2 = DateTime.Now;
            DateTime dc3 = DateTime.Now;

            dc1 = d1;
            dc2 = d2;
            dc3 = d1.AddHours(1);
            if (dc1 >= DateTime.Now && dc2 >= DateTime.Now)
            {
                while (dc1 != dc2)
                {

                    using (NpgsqlConnection conn = GetConnection())
                    {

                        string insert = "INSERT INTO Booking_Table (userid,consultant_id,doctor_id,starttime,endtime) VALUES (@p,@consultType,@Doctor,@timeslot,@timeslot2)";
                        NpgsqlCommand cmd = new NpgsqlCommand(insert, conn);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new NpgsqlParameter("@consultType", a));
                        cmd.Parameters.AddWithValue("@Doctor", b);
                        cmd.Parameters.AddWithValue("@p", k);
                        cmd.Parameters.AddWithValue("@timeslot", dc1);
                        cmd.Parameters.AddWithValue("@timeslot2", dc3);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();   
                        dc1 = dc1.AddHours(1);
                        dc3 = dc3.AddHours(1);
                    }
                }
                MessageBox.Show("Successfully Registered", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
               
            }

          
            else
            {
                MessageBox.Show("Selected time has already passed", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);



            }

        }

        public DateTime? black(string? s)
        {

            int b = 0;
            using (var connection = GetConnection()) // You need to replace GetConnection() with your actual connection creation logic
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT doctor_Id from doctor_table where @d=Doctor_Name";
                command.Parameters.Add(new NpgsqlParameter("@d", s));
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Add the value from the "Doctor_Name" column to the j list
                        b = reader.GetInt32(0);

                        // Assuming the column is of string type
                    }
                }
            }

            List<DateTime> slotlist = new List<DateTime>();
            using (var connection = GetConnection()) // You need to replace GetConnection() with your actual connection creation logic
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT StartTime FROM Booking_Table where @doctorid=Doctor_Id AND Deleted_TimeStamp is NULL";
                command.Parameters.Add(new NpgsqlParameter("@doctorid", b));
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Add the value from the "Doctor_Name" column to the j list
                        slotlist.Add(reader.GetDateTime(0));

                        // Assuming the column is of string type
                    }
                }
            }
            slotlist.Sort();
            DateTime? date1 = null;
            foreach (DateTime dat in slotlist)
            {
                int occurrences = slotlist.Count(dt => dt.Date == dat.Date);
                if (occurrences >= 8)
                {
                    date1=dat;

                }
            }
            return date1;
        }
    }
}
