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
    /// <summary>
    /// Get The Consultation Description
    /// </summary>
    public class BookRepo : RepositoryBase
    {
        public List<string> slotList = new List<string>();
        public List<string> slotList2 = new List<string>();
        public List<string> BookGetCo()
        {
            List<string> list = new List<string>();

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
                        
                        list.Add(reader.GetString(0)); 
                    }
                }
            }
            return list;
        }

        //Get the Doctor Names
        public List<string> BookGetDoc(string selectedConsultation)
        {
            int tempConsultID = 0;
            using (var connection = GetConnection()) 
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT consultant_id from consultant_Type where @consultant_Desc = consultant_desc";
                command.Parameters.Add(new NpgsqlParameter("@consultant_Desc", selectedConsultation));
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        tempConsultID = reader.GetInt32(0);

                        
                    }
                }
            }
           List<string> List = new List<string>();  
            using (var connection = GetConnection())
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT doctor_Name from doctor_table where @Consultant_Id=consultant_id";
                command.Parameters.Add(new NpgsqlParameter("@Consultant_Id", tempConsultID));
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        List.Add(reader.GetString(0));  

                        
                    }
                }
            }
            return List;
        }

        //Get the available time of specific doctors
        public void BookGetTime(string doc, DateTime selectedDate)
        {
        
       
            DateTime ne = DateTime.Now;
         
            int Id = 0;
            using (var connection = GetConnection()) 
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT doctor_Id from doctor_table where @DocName=Doctor_Name";
                command.Parameters.Add(new NpgsqlParameter("@DocName", doc));
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                       
                        Id = reader.GetInt32(0);

                      
                    }
                }
            }
            using (var connection = GetConnection()) 
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT available_starttime, available_endtime FROM doctor_availability WHERE DATE(doctor_availability.available_starttime) = @Date AND DATE(doctor_availability.available_endtime) = @Date AND doctor_availability.doctor_id = @DocId EXCEPT SELECT available_starttime, available_endtime FROM booking_table, doctor_availability WHERE booking_table.StartTime = available_starttime AND booking_table.doctor_id = @DocId AND booking_table.EndTime = available_endtime AND Deleted_TimeStamp IS NULL ORDER BY available_starttime, available_endtime;";
                command.Parameters.Add(new NpgsqlParameter("@Date", selectedDate));
                command.Parameters.Add(new NpgsqlParameter("@DocId", Id));
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                       
                        TimeSpan timeSpan1= reader.GetDateTime(0).TimeOfDay;
                        TimeSpan timeSpan2= reader.GetDateTime(1).TimeOfDay;
                      
                        string Format1 = $"{timeSpan1.Hours:D2}:{timeSpan1.Minutes:D2}";
                        string Format2 = $"{timeSpan2.Hours:D2}:{timeSpan2.Minutes:D2}";
                       
                        slotList.Add(Format1);
                        slotList2.Add(Format2); 

                       
                    }
                }
            }

        }
        //Add The bookings to DB

        public void AddBook(string selectedDep, DateTime selectedDate, string doc, DateTime d1, DateTime d2)
        {
            int tempConsultID = 0;
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

                        tempConsultID = reader.GetInt32(0);

                       
                    }
                }
            }
            int tempDoctorID = 0;
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

                        tempDoctorID = reader.GetInt32(0);

                       
                    }
                }
            }
            int userId = 0;
            using (var connection = GetConnection()) 
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT userid from userdetails where active_session = 1";
              
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                       
                        userId = reader.GetInt32(0);

                        
                    }
                }
            }
            DateTime TempDate1 = DateTime.Now;
            DateTime TempDate2 = DateTime.Now;
            DateTime TempDate3 = DateTime.Now;

            TempDate1 = d1;
            TempDate2 = d2;
            TempDate3 = d1.AddHours(1);
            if (TempDate1 >= DateTime.Now && TempDate2 >= DateTime.Now)
            {
                while (TempDate1 != TempDate2)
                {

                    using (NpgsqlConnection conn = GetConnection())
                    {

                        string insert = "INSERT INTO Booking_Table (userid,consultant_id,doctor_id,starttime,endtime) VALUES (@userId,@consultType,@Doctor,@timeslot,@timeslot2)";
                        NpgsqlCommand cmd = new NpgsqlCommand(insert, conn);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new NpgsqlParameter("@consultType", tempConsultID));
                        cmd.Parameters.AddWithValue("@Doctor", tempDoctorID);
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@timeslot", TempDate1);
                        cmd.Parameters.AddWithValue("@timeslot2", TempDate3);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        TempDate1 = TempDate1.AddHours(1);
                        TempDate3 = TempDate3.AddHours(1);
                    }
                }
                MessageBox.Show("Successfully Registered", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
               
            }

          
            else
            {
                MessageBox.Show("Selected time has already passed", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);



            }

        }

        //Blacking out of Dates

        public DateTime? black(string? s)
        {

            int b = 0;
            using (var connection = GetConnection()) 
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
                        
                        b = reader.GetInt32(0);

                      
                    }
                }
            }

            List<DateTime> slotList = new List<DateTime>();
            using (var connection = GetConnection()) 
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT StartTime FROM Booking_Table where @doctorId=Doctor_Id AND Deleted_TimeStamp is NULL";
                command.Parameters.Add(new NpgsqlParameter("@doctorId", b));
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                       
                        slotList.Add(reader.GetDateTime(0));

                       
                    }
                }
            }
            slotList.Sort();
            DateTime? date1 = null;
            foreach (DateTime dat in slotList)
            {
                int occurrences = slotList.Count(dt => dt.Date == dat.Date);
                if (occurrences >= 8)
                {
                    date1=dat;

                }
            }
            return date1;
        }
    }
}
