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
using static System.Net.Mime.MediaTypeNames;

namespace MVVM_App.Repositories
{

    public class AddRepo : RepositoryBase, IAddDocRepo
    {
        public List<string> get()
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

        public List<string> getco()
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
                            count = reader.GetInt32(0);
                        }
                    }
                    if (count > 0)
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

        public void addDoctor(string text, string v)
        {
            int a = 0;
            using (var connection = GetConnection()) // You need to replace GetConnection() with your actual connection creation logic
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT consultant_id from consultant_Type where @c = consultant_desc";
                command.Parameters.Add(new NpgsqlParameter("@c", v));
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Add the value from the "Doctor_Name" column to the j list
                        a = reader.GetInt32(0);

                        // Assuming the column is of string type
                    }
                }
            }
            int? b = null;
            using (var connection = GetConnection()) // You need to replace GetConnection() with your actual connection creation logic
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT doctor_Id from doctor_table where @d=Doctor_Name";
                command.Parameters.Add(new NpgsqlParameter("@d", text));
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
            if (b != null)
            {
                MessageBox.Show("This name already exists. Please enter a valid Full Name", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                using (NpgsqlConnection conn = GetConnection())
                {
                    string insert = "insert into doctor_table(Doctor_name,consultant_id) values(@Doctorn,@Consult_Id)";
                    NpgsqlCommand cmd = new NpgsqlCommand(insert, conn);
                    cmd.Parameters.Add(new NpgsqlParameter("@Consult_Id", a));

                    cmd.Parameters.Add(new NpgsqlParameter("@Doctorn", text));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Added !!", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    conn.Close();
                }
            }

        }

        public List<string> selectionchangedoc1(string v)
        {
            int b = 0;
            using (var connection = GetConnection()) // You need to replace GetConnection() with your actual connection creation logic
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT doctor_Id from doctor_table where @d=Doctor_Name";
                command.Parameters.Add(new NpgsqlParameter("@d", v));
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
            int a = 0;
            using (var connection = GetConnection()) // You need to replace GetConnection() with your actual connection creation logic
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT consultant_id from doctor_table where @c = doctor_Id";
                command.Parameters.Add(new NpgsqlParameter("@c", b));
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Add the value from the "Doctor_Name" column to the j list
                        a = reader.GetInt32(0);

                        // Assuming the column is of string type
                    }
                }
            }
            List<string> list2 = new List<string>();

            using (var connection = GetConnection()) // You need to replace GetConnection() with your actual connection creation logic
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT consultant_desc from consultant_type where consultant_id = @d";
                command.Parameters.Add(new NpgsqlParameter("@d", a));
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

        public void selectionconchanged(DateTime dat1, DateTime dat2, string s, DateTime startDate, DateTime EndDate)
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
            int matchingCount = 0;
            using (var connection = GetConnection()) // You need to replace GetConnection() with your actual connection creation logic
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT COUNT(*) FROM doctor_availability WHERE TO_CHAR(available_starttime, 'YYYY-MM-DD HH24:MI:SS') = @SelectedDateTime  AND doctor_id = @docid;";
                command.Parameters.Add(new NpgsqlParameter("@SelectedDateTime", dat1.ToString("yyyy-MM-dd HH:mm:ss")));
                command.Parameters.Add(new NpgsqlParameter("@docid", b));
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Add the value from the "Doctor_Name" column to the j list
                        matchingCount = reader.GetInt32(0);

                        // Assuming the column is of string type
                    }
                }
            }
            if (matchingCount > 0)
            {
                MessageBox.Show("Already added timings", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }




        public void ischecked(DateTime dat1, DateTime dat2, string s, DateTime startDate, DateTime EndDate, string FromTime, string EndTime, int t)
        {
            DateTime d1 = DateTime.Now;
            DateTime d2 = DateTime.Now;
            DateTime dc1 = DateTime.Now;
            DateTime dc2 = DateTime.Now;
            DateTime dc3 = DateTime.Now;
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
            for (DateTime currentDate = startDate; currentDate <= EndDate; currentDate = currentDate.AddDays(1))
            {



                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    if (FromTime != null)
                    {
                        string s1 = FromTime;
                        DateTime dt;
                        DateTime.TryParse(s1, out dt);
                        string Time = dt.ToString("HH:mm:ss");
                        d1 = DateTime.Parse(currentDate.ToString("dd/MM/yyyy ") + Time);
                    }
                    if (EndTime != null)
                    {
                        string s1 = EndTime;
                        DateTime dt1;
                        DateTime.TryParse(s1, out dt1);
                        string Time = dt1.ToString("HH:mm:ss");
                        d2 = DateTime.Parse(currentDate.ToString("dd/MM/yyyy ") + Time);
                    }

                    if (t == 1)
                    {
                        dc1 = d1;
                        dc2 = d2;
                        dc3 = d1.AddHours(1);
                        while (dc1 != dc2)
                        {
                            using (NpgsqlConnection conn = GetConnection())
                            {
                                string insert = "INSERT INTO doctor_availability (doctor_id,available_starttime,available_endtime) VALUES (@doctorid,@starttime,@endtime)";
                                NpgsqlCommand cmd = new NpgsqlCommand(insert, conn);
                                cmd.Parameters.Add(new NpgsqlParameter("@doctorid", b));
                                cmd.Parameters.Add(new NpgsqlParameter("@starttime", dc1));
                                cmd.Parameters.Add(new NpgsqlParameter("@endtime", dc3));

                                conn.Open();
                                cmd.ExecuteNonQuery();

                                conn.Close();
                            }

                            dc1 = dc1.AddHours(1);
                            dc3 = dc3.AddHours(1);
                        }

                        MessageBox.Show("Successfully Registered", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        DateTime d3 = DateTime.Now;
                        DateTime d4 = DateTime.Now;
                        DateTime d5 = DateTime.Now;
                        DateTime d6 = DateTime.Now;
                        DateTime d7 = DateTime.Now;
                        DateTime d8 = DateTime.Now;
                        DateTime d9 = DateTime.Now;
                        DateTime ds = DateTime.Now;
                        DateTime df = DateTime.Now;
                        using (NpgsqlConnection conn = GetConnection())
                        {
                            string insert = "INSERT INTO doctor_availability (Doctor_Id,available_starttime,available_endtime) VALUES (@doctorid,@starttime1,@endtime1),(@doctorid,@endtime1,@starttime2),(@doctorid,@starttime2,@endtime2),(@doctorid,@endtime2,@starttime3),(@doctorid,@starttime3,@endtime3),(@doctorid,@endtime3,@starttime4),(@doctorid,@starttime4,@endtime4),(@doctorid,@endtime4,@starttime5)";
                            NpgsqlCommand cmd = new NpgsqlCommand(insert, conn);
                            cmd.Parameters.Add(new NpgsqlParameter("@doctorid", b));
                            d3 = DateTime.Parse(currentDate.ToString("dd/MM/yyyy ") + "10:00:00.000");
                            d4 = DateTime.Parse(currentDate.ToString("dd/MM/yyyy ") + "11:00:00.000");
                            d5 = DateTime.Parse(currentDate.ToString("dd/MM/yyyy ") + "12:00:00.000");
                            d6 = DateTime.Parse(currentDate.ToString("dd/MM/yyyy ") + "13:00:00.000");
                            d7 = DateTime.Parse(currentDate.ToString("dd/MM/yyyy ") + "14:00:00.000");
                            d8 = DateTime.Parse(currentDate.ToString("dd/MM/yyyy ") + "15:00:00.000");
                            d9 = DateTime.Parse(currentDate.ToString("dd/MM/yyyy ") + "16:00:00.000");
                            ds = DateTime.Parse(currentDate.ToString("dd/MM/yyyy ") + "17:00:00.000");
                            df = DateTime.Parse(currentDate.ToString("dd/MM/yyyy ") + "18:00:00.000");
                            cmd.Parameters.Add(new NpgsqlParameter("@starttime1", d3));
                            cmd.Parameters.Add(new NpgsqlParameter("@endtime1", d4));
                            cmd.Parameters.Add(new NpgsqlParameter("@starttime2", d5));
                            cmd.Parameters.Add(new NpgsqlParameter("@endtime2", d6));
                            cmd.Parameters.Add(new NpgsqlParameter("@starttime3", d7));
                            cmd.Parameters.Add(new NpgsqlParameter("@endtime3", d8));
                            cmd.Parameters.Add(new NpgsqlParameter("@starttime4", d9));
                            cmd.Parameters.Add(new NpgsqlParameter("@endtime4", ds));
                            cmd.Parameters.Add(new NpgsqlParameter("@starttime5", df));
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }


                }
            }


        }

        public List<string> UpdateDoc()
        {
            throw new NotImplementedException();
        }
    }
}
        //public void UpdateDoctor()
        //{
        //    List<string> list = new List<string>();
        //    List<string> Doclist = new List<string>();
        //    using (NpgsqlConnection conn = GetConnection())
        //    {
        //        conn.Open();
        //        NpgsqlCommand cmd = new NpgsqlCommand("SELECT CONSULTANT_DESC FROM CONSULTANT_TYPE", conn);
        //    using (NpgsqlDataReader reader = cmd.ExecuteReader())
        //    {
        //        while (reader.Read())
        //        {
        //           // list.Add(reader.GetTimeSpan(0));
        //        }
        //    }
        //}
        //        //return list2;
                  //    }




//    SqlCommand cmd = new SqlCommand("SELECT CONSULTANT_DESC FROM CONSULTANT_TYPE", con);
//            SqlCommand doccmd = new SqlCommand("Select Doctor_Name from Doctor", con);
//            using (cmd)
//            {
//                con.Open();
//                using (SqlDataReader reader = cmd.ExecuteReader())
//                {
//                    while (reader.Read())
//                    {
//                        list.Add(reader.GetString(0));
//                    }
//                }
//                con.Close();
//            }
//            ConsultType.ItemsSource = list;
//            using (doccmd)
//            {
//                con.Open();
//                using (SqlDataReader reader = doccmd.ExecuteReader())
//                {
//                    while (reader.Read())
//                    {
//                        Doclist.Add(reader.GetString(0));
//                    }
//                }
//                con.Close();
//            }
//            DoctorType.ItemsSource = Doclist;
//        }
//    }
//}

//using (var connection = GetConnection()) // You need to replace GetConnection() with your actual connection creation logic
//using (var command = new NpgsqlCommand())
//{
//    connection.Open();
//    command.Connection = connection;
//    command.CommandText = "SELECT demoendtime from demo_time";

//    using (NpgsqlDataReader reader = command.ExecuteReader())
//    {
//        while (reader.Read())
//        {
//            // Add the value from the "Doctor_Name" column to the j list
//            list2.Add(reader.GetTimeSpan(0)); // Assuming the column is of string type
//        }
//    }
//}
//return list2;
//        }
//List<DataGridItem> NewList = new List<DataGridItem>();
//DataGridItem item = null; ;
//using (NpgsqlConnection conn = GetConnection())
//{
//    conn.Open();
//    string view = "SELECT NAME,DOCTOR_NAME,STARTTIME, ENDTIME, CONSULTANT_DESC FROM USERDETAILS INNER JOIN BOOKING_TABLE ON USERDETAILS.USERID = BOOKING_TABLE.USERID INNER JOIN DOCTOR_TABLE ON " +
//        "BOOKING_TABLE.DOCTOR_ID = DOCTOR_TABLE.DOCTOR_ID INNER JOIN CONSULTANT_TYPE ON DOCTOR_TABLE.CONSULTANT_ID = CONSULTANT_TYPE.CONSULTANT_ID WHERE DELETED_TIMESTAMP IS NULL AND DATE(STARTTIME)>=CURRENT_DATE";
//    NpgsqlCommand cmd = new NpgsqlCommand(view, conn);
//    using (var reader = cmd.ExecuteReader())
//    {
//        while (reader.Read())
//        {
//            item = new DataGridItem()
//            {
//                Name = reader.GetString("NAME"),
//                DoctorName = reader.GetString("DOCTOR_NAME"),
//                StartTime = reader.GetDateTime("STARTTIME"),
//                EndTime = reader.GetDateTime("ENDTIME"),
//                Consultant_Desc = reader.GetString("CONSULTANT_DESC"),
//            };
//            NewList.Add(item);
//        }
//    }

//}
//return NewList;