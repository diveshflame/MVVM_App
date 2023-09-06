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
using System.Windows.Navigation;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace MVVM_App.Repositories
{

    public class AddRepo : RepositoryBase, IAddDocRepo 
    {
        //get doctor names
        public List<string> get()
        {
            List<string> list2 = new List<string>();

            using (var connection = GetConnection()) 
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT doctor_name from doctor_table";

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

        //To get consultation Descriptions
        public List<string> getCo()
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




        //Add Consultation function
        public void add(string id)
        {
           
           
                int count = 0;
                using (var connection = GetConnection()) 
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


        //Link with demotime

        public List<string> startT()
        {
            List<string> list2 = new List<string>();

            using (var connection = GetConnection())
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT demostarttime from demo_time";

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                       
                        TimeSpan timeSpan = reader.GetTimeSpan(0);
                        string hhmmFormat = $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}";
                    
                        list2.Add(hhmmFormat);
                    }
                }
            }
            return list2;
        }
       //Get End Time

        public List<string> EndT()
        {
            List<string> list2 = new List<string>();

            using (var connection = GetConnection()) 
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT demoendtime from demo_time";

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        TimeSpan timeSpan = reader.GetTimeSpan(0);
                        string hhmmFormat = $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}";
                       
                        list2.Add(hhmmFormat);// Assuming the column is of string type
                    }
                }
            }
            return list2;
        }
        //Insert New Doctor 
        public void addDoctor(string text, string consultantDesc)
        {
            int tempConsultID = 0;
            using (var connection = GetConnection()) 
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT consultant_id from consultant_Type where @consultantDesc = consultant_desc";
                command.Parameters.Add(new NpgsqlParameter("@consultantDesc", consultantDesc));
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        tempConsultID = reader.GetInt32(0);

                 
                    }
                }
            }
            int? docId = null;
            using (var connection = GetConnection()) 
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT doctor_Id from doctor_table where @Doctor_Name=doctor_name";
                command.Parameters.Add(new NpgsqlParameter("@Doctor_Name", text));
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        docId = reader.GetInt32(0);

                  
                    }
                }
            }
            if (docId != null)
            {
                MessageBox.Show("This name already exists. Please enter a valid Full Name", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                using (NpgsqlConnection conn = GetConnection())
                {
                    string insert = "insert into doctor_table(Doctor_name,consultant_id) values(@DoctorN,@Consult_Id)";
                    NpgsqlCommand cmd = new NpgsqlCommand(insert, conn);
                    cmd.Parameters.Add(new NpgsqlParameter("@Consult_Id", tempConsultID));

                    cmd.Parameters.Add(new NpgsqlParameter("@DoctorN", text));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Added !!", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    conn.Close();
                }
            }

        }

        //when consultation selection changes get Doctor names for it
        public List<string> selectionChangeDoc1(string consultant_desc)
        {
            int TempConsultId = 0;
            using (var connection = GetConnection()) 
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT consultant_id from consultant_Type where @ConsultDesc = consultant_desc";
                command.Parameters.Add(new NpgsqlParameter("@ConsultDesc", consultant_desc));
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        TempConsultId = reader.GetInt32(0);

                        
                    }
                }
            }
         
            List<string> list2 = new List<string>();

            using (var connection = GetConnection()) 
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT doctor_name from doctor_table where consultant_id = @ConsultId";
                command.Parameters.Add(new NpgsqlParameter("@ConsultId", TempConsultId));
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

        //When selected Date changes check if already booked timings or not
        public int selectionConChanged(DateTime dat1, DateTime dat2,string docName,DateTime startDate,DateTime EndDate)
        {
            int keepTab = 0;  //To check if matching count value


            int TempDocId = 0;
            using (var connection = GetConnection())
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT doctor_Id from doctor_table where @d=Doctor_Name";
                command.Parameters.Add(new NpgsqlParameter("@d", docName));
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                      
                        TempDocId = reader.GetInt32(0);

                        
                    }
                }
            }
            int matchingCount = 0;
            using (var connection = GetConnection()) 
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT COUNT(*) FROM doctor_availability WHERE TO_CHAR(available_starttime, 'YYYY-MM-DD HH24:MI:SS') = @SelectedDateTime  AND doctor_id = @docid;";
                command.Parameters.Add(new NpgsqlParameter("@SelectedDateTime", dat1.ToString("yyyy-MM-dd HH:mm:ss")));
                command.Parameters.Add(new NpgsqlParameter("@docid", TempDocId));
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        matchingCount = reader.GetInt32(0);

                        
                    }
                }
            }
            if (matchingCount > 0)
            {
                keepTab = 1;
               
                MessageBox.Show("Already added timings", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
          
             
            }

            return keepTab;


        }

        //If check mark returns true then insert from 10-6 else specific selected 


        public void isChecked(DateTime dat1, DateTime dat2, string doc_name, DateTime startDate, DateTime EndDate, string FromTime, string EndTime, int t)
        {
            DateTime d1 = DateTime.Now;
            DateTime d2 = DateTime.Now;
            DateTime TempDate1 = DateTime.Now;
            DateTime TempDate2 = DateTime.Now;
            DateTime TempDate3 = DateTime.Now;
            int TempDocId = 0;
            
                using (var connection = GetConnection()) 
                using (var command = new NpgsqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT doctor_Id from doctor_table where @doc_name=Doctor_Name";
                    command.Parameters.Add(new NpgsqlParameter("@doc_name", doc_name));
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                        TempDocId = reader.GetInt32(0);

                            
                        }
                    }
                }
                //If not weekends
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
                        TempDate1 = d1;
                        TempDate2 = d2;
                        TempDate3 = d1.AddHours(1);
                            while (TempDate1 != TempDate2)
                            {
                                using (NpgsqlConnection conn = GetConnection())
                                {
                                    string insert = "INSERT INTO doctor_availability (doctor_id,available_starttime,available_endtime) VALUES (@doctorid,@starttime,@endtime)";
                                    NpgsqlCommand cmd = new NpgsqlCommand(insert, conn);
                                    cmd.Parameters.Add(new NpgsqlParameter("@doctorid", TempDocId));
                                    cmd.Parameters.Add(new NpgsqlParameter("@starttime", TempDate1));
                                    cmd.Parameters.Add(new NpgsqlParameter("@endtime", TempDate3));

                                    conn.Open();
                                    cmd.ExecuteNonQuery();

                                    conn.Close();
                                }

                            TempDate1 = TempDate1.AddHours(1);
                            TempDate3 = TempDate3.AddHours(1);
                            }

                       
                    }
                        //Insert values from starting to ending specified hours
                    else
                    {
                        TimeSpan startTime = TimeSpan.FromHours(10);
                        TimeSpan endTime = TimeSpan.FromHours(18);
                        TimeSpan slotDuration = TimeSpan.FromHours(1);

                        using (NpgsqlConnection conn = GetConnection())
                        {
                            string insert = "INSERT INTO doctor_availability (Doctor_Id, available_starttime, available_endtime) VALUES (@doctorid, @starttime, @endtime)";
                            conn.Open();

                            using (NpgsqlCommand cmd = new NpgsqlCommand(insert, conn))
                            {
                                cmd.Parameters.AddWithValue("@doctorid", TempDocId);

                                while (startTime + slotDuration <= endTime)
                                {
                                    DateTime startDateTime = currentDate.Date + startTime;
                                    DateTime endDateTime = startDateTime + slotDuration;

                                    cmd.Parameters.AddWithValue("@starttime", startDateTime);
                                    cmd.Parameters.AddWithValue("@endtime", endDateTime);

                                    cmd.ExecuteNonQuery();

                                    startTime += slotDuration;

                                    cmd.Parameters.RemoveAt("@starttime");
                                    cmd.Parameters.RemoveAt("@endtime");
                                }
                            }
                        }
                    }
                  

                }
            }
            MessageBox.Show("Successfully Registered", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);

        }

       /// <summary>
       /// Update Doctor 
       /// </summary>
       /// 
        //Get DoctorId using DoctorId
        

        //Function to only get the doctor ID
        public int GetDoctorId(string DocName)
        {
            int Doc_Id = 0;

            using (var con = GetConnection())
            {
                con.Open();
                NpgsqlCommand getDoc_Id = new NpgsqlCommand("Select Doctor_Id from Doctor_Table where Doctor_Name=@docName ", con);
                getDoc_Id.Parameters.AddWithValue("@docName", DocName);

                using (getDoc_Id)
                {
                    using (NpgsqlDataReader reader = getDoc_Id.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Doc_Id = reader.GetInt32(0);
                        }
                    }

                }
            }

                return Doc_Id;
            }

            /// <summary>
            /// Get Consultation id using consultantName
            /// </summary>
            /// <param name="ConsultName"></param>
            /// <returns></returns>
            /// 
            //Function to get consultant ID
            public int GetConsultantId(string ConsultName)
            {
                int Consult_Id = 0;

            using (var con = GetConnection())
            {
                con.Open();
                NpgsqlCommand getCon_Id = new NpgsqlCommand("Select consultant_Id from Consultant_Type where consultant_Desc=@Conname;", con);
                getCon_Id.Parameters.AddWithValue("@Conname", ConsultName);
                using (getCon_Id)
                {
                    using (NpgsqlDataReader reader = getCon_Id.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Consult_Id = reader.GetInt32(0);
                        }
                    }

                }
            }
                  return Consult_Id;
            }
        /// <summary>
        /// Update doctor if the doctor not there in the doctor availability table
        /// </summary>
        /// <param name="Doc_id"></param>
        /// <param name="Consult_id"></param>
        /// <returns></returns>
           public bool UpdateDoc(int Doc_id, int Consult_id)
           {
              bool IsValid = false;
              int? getCheckedDocId = null;
              using (var con = GetConnection()) 
              {
                con.Open();
                NpgsqlCommand checkDoc_id = new NpgsqlCommand("Select Doctor_Id from Doctor_Availability where Doctor_id=@d", con);
                checkDoc_id.Parameters.AddWithValue("@d", Doc_id);
                using (checkDoc_id)
                {
                    
                    using (NpgsqlDataReader reader = checkDoc_id.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            getCheckedDocId = reader.GetInt32(0);
                        }

                    }
                }
                if (getCheckedDocId != null)
                {
                    MessageBox.Show("You Cannot update!!!!The doctor is already booked", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    IsValid = true;
                }
                else
                {
                    NpgsqlCommand updateCmd = new NpgsqlCommand("UPDATE Doctor_Table SET consultant_Id =@cId WHERE Doctor_id=@DocId and Doctor_id  Not in " +
                        "(Select Doctor_id from Doctor_Availability);", con);
                    updateCmd.Parameters.AddWithValue("@cId", Consult_id);
                    updateCmd.Parameters.AddWithValue("@DocId", Doc_id);
                    
                    updateCmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Successfully Updated", "Success", MessageBoxButton.OK, MessageBoxImage.Information);


                }
                IsValid = true;
            }
              return IsValid;
        }

        //Get dates for blackout
        public List<DateTime> black(string? s)
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

            List<DateTime> slotlist = new List<DateTime>();
            using (var connection = GetConnection()) 
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT available_starttime FROM Doctor_Availability where @doctorid=Doctor_Id";
                command.Parameters.Add(new NpgsqlParameter("@doctorid", b));
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        slotlist.Add(reader.GetDateTime(0));

                      
                    }
                }
            }
            slotlist.Sort();
            List<DateTime> date1 = new List<DateTime>();
            foreach (DateTime dat in slotlist)
            {
                int occurrences = slotlist.Count(dt => dt.Date == dat.Date);
                if (occurrences >= 8)
                {
                    date1.Add(dat); 
                    
                }

            }
            return date1;
        

    }
    }
}



