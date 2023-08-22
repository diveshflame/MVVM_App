using MVVM_App.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_App.Repositories;
using System.Windows.Documents;
using Microsoft.VisualBasic.ApplicationServices;
using System.Windows;

namespace MVVM_App.Repositories
{
    public class DataGridRepository : RepositoryBase, IAdminBooking
    {
     
        public List<DataGridItem> View()
        {
            List<DataGridItem> NewList =new List<DataGridItem>();
            DataGridItem item = null; ;
            using(NpgsqlConnection conn= GetConnection())
            {
                conn.Open();
                string view = "SELECT NAME,DOCTOR_NAME,STARTTIME, ENDTIME, CONSULTANT_DESC FROM USERDETAILS INNER JOIN BOOKING_TABLE ON USERDETAILS.USERID = BOOKING_TABLE.USERID INNER JOIN DOCTOR_TABLE ON " +
                    "BOOKING_TABLE.DOCTOR_ID = DOCTOR_TABLE.DOCTOR_ID INNER JOIN CONSULTANT_TYPE ON DOCTOR_TABLE.CONSULTANT_ID = CONSULTANT_TYPE.CONSULTANT_ID WHERE DELETED_TIMESTAMP IS NULL AND DATE(STARTTIME)>=CURRENT_DATE";
                NpgsqlCommand cmd = new NpgsqlCommand(view, conn);
                using(var reader = cmd.ExecuteReader()) 
                {
                    while(reader.Read())
                    {
                        item = new DataGridItem()
                        {
                            Name = reader.GetString("NAME"),
                            DoctorName = reader.GetString("DOCTOR_NAME"),
                            StartTime = reader.GetDateTime("STARTTIME"),
                            EndTime = reader.GetDateTime("ENDTIME"),
                            Consultant_Desc = reader.GetString("CONSULTANT_DESC"),
                        };
                        NewList.Add(item);
                    }
                }

            }
            return NewList;
        }

        public List<DataGridItem> ViewHistory()
        {
            List<DataGridItem> NewList = new List<DataGridItem>();
            DataGridItem item = null; ;
            using (NpgsqlConnection conn = GetConnection())
            {
                conn.Open();
                string view = "SELECT NAME,DOCTOR_NAME,STARTTIME, ENDTIME, CONSULTANT_DESC FROM USERDETAILS INNER JOIN BOOKING_TABLE ON USERDETAILS.USERID = BOOKING_TABLE.USERID INNER JOIN DOCTOR_TABLE ON " +
                    "BOOKING_TABLE.DOCTOR_ID = DOCTOR_TABLE.DOCTOR_ID INNER JOIN CONSULTANT_TYPE ON DOCTOR_TABLE.CONSULTANT_ID = CONSULTANT_TYPE.CONSULTANT_ID AND DELETED_TIMESTAMP IS NULL ";
                NpgsqlCommand cmd = new NpgsqlCommand(view, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        item = new DataGridItem()
                        {
                            Name = reader.GetString("NAME"),
                            DoctorName = reader.GetString("DOCTOR_NAME"),
                            StartTime = reader.GetDateTime("STARTTIME"),
                            EndTime = reader.GetDateTime("ENDTIME"),
                            Consultant_Desc = reader.GetString("CONSULTANT_DESC"),
                        };
                        NewList.Add(item);
                    }
                }

            }
            return NewList;
        }

        public List<DataGridItem> ViewToday()
        {
            List<DataGridItem> NewList = new List<DataGridItem>();
            DataGridItem item = null; ;
            using (NpgsqlConnection conn = GetConnection())
            {
                conn.Open();
                string view = "SELECT NAME,DOCTOR_NAME,STARTTIME, ENDTIME, CONSULTANT_DESC FROM USERDETAILS INNER JOIN BOOKING_TABLE ON USERDETAILS.USERID = BOOKING_TABLE.USERID INNER JOIN DOCTOR_TABLE ON " +
                    "BOOKING_TABLE.DOCTOR_ID = DOCTOR_TABLE.DOCTOR_ID INNER JOIN CONSULTANT_TYPE ON DOCTOR_TABLE.CONSULTANT_ID = CONSULTANT_TYPE.CONSULTANT_ID WHERE DELETED_TIMESTAMP IS NULL AND DATE(STARTTIME)=CURRENT_DATE";
                NpgsqlCommand cmd = new NpgsqlCommand(view, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        item = new DataGridItem()
                        {
                            Name = reader.GetString("NAME"),
                            DoctorName = reader.GetString("DOCTOR_NAME"),
                            StartTime = reader.GetDateTime("STARTTIME"),
                            EndTime = reader.GetDateTime("ENDTIME"),
                            Consultant_Desc = reader.GetString("CONSULTANT_DESC"),
                        };
                        NewList.Add(item);
                    }
                }

            }
            return NewList;
        }

        //User View Bookings
        public List<DataGridItem> ViewUserBookings()
        {
            List<DataGridItem> NewList = new List<DataGridItem>();
            DataGridItem item = null; 
            using (NpgsqlConnection conn = GetConnection())
            {
                conn.Open();
                string View = @"SELECT BOOKING_ID,
                                    BOOKING_TABLE.DOCTOR_ID,
	                                CONSULTANT_DESC,
	                                DOCTOR_NAME,
	                                STARTTIME,
	                                ENDTIME
                               FROM BOOKING_TABLE
                               JOIN DOCTOR_TABLE ON BOOKING_TABLE.DOCTOR_ID = DOCTOR_TABLE.DOCTOR_ID
                               JOIN CONSULTANT_TYPE ON BOOKING_TABLE.CONSULTANT_ID = CONSULTANT_TYPE.CONSULTANT_ID
                               WHERE DELETED_TIMESTAMP IS NULL AND DATE(STARTTIME)>= CURRENT_DATE AND BOOKING_TABLE.USERID IN
                                    (SELECT USERID
                                        FROM USERDETAILS
                                        WHERE ACTIVE_SESSION = 0)";
                NpgsqlCommand cmd = new NpgsqlCommand(View, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        item = new DataGridItem()
                        {
                            DoctorName = reader.GetString("DOCTOR_NAME"),
                            StartTime = reader.GetDateTime("STARTTIME"),
                            EndTime = reader.GetDateTime("ENDTIME"),
                            Consultant_Desc = reader.GetString("CONSULTANT_DESC"),
                        };
                        NewList.Add(item);
                    }
                }

            }
            return NewList;
        }

        public List<DataGridItem> ViewUsersHistory()
        {
            throw new NotImplementedException();
        }

        public List<DataGridItem> ViewUsersTodayBooking()
        {
            throw new NotImplementedException();
        }
        public void Delete()
        {

            //if (MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            //{
            //    DataRowView row = (DataRowView)mytable.SelectedItem;
            //    string pk = row["Booking_Id"].ToString();
            //    DateTime cancelDate = (DateTime)row["StartTime"];
            //    string docid = row["Doctor_Id"].ToString();
            //    DateTime starttime = DateTime.Parse(row["StartTime"].ToString());
            //    DateTime endtime = DateTime.Parse(row["EndTime"].ToString());
            //    var Differencedate = cancelDate - DateTime.Now;
            //    if (Differencedate.Days < 2)
            //    {
            //        //btnDelete.IsEnabled = false;
            //        MessageBox.Show("You cannnot Cancel the appointment 48 hours prior to the appointment.Please Contact the doctor's office.", "Detail");
            //    }
            //    else
            //    {
            //        row.Delete();
            //        DateTime dateTime = DateTime.Now;

            //        using (conn = new SqlConnection("Data Source=.;Initial Catalog=UserBase;Integrated Security=True"))
            //        {
            //            conn.Open();
            //            string View = "update Booking_Table set Daleted_TimeStamp=" + "'" + dateTime.ToString("yyyy-MM-dd HH:mm:ss") + "'" + "where Booking_Id=" + pk;
            //            cmd = new SqlCommand(View, conn);
            //            DataTable dt = new DataTable();
            //            cmd.ExecuteNonQuery();
            //            dt.Load(cmd.ExecuteReader());
            //            string delete = "insert into DoctorAvailability values (@doctor,@starttime,@endtime)";
            //            SqlCommand cmd2 = new SqlCommand(delete, conn);
            //            cmd2.Parameters.AddWithValue("@doctor", docid);
            //            cmd2.Parameters.AddWithValue("@starttime", starttime);
            //            cmd2.Parameters.AddWithValue("@endtime", endtime);
            //            cmd2.ExecuteNonQuery();
            //            mytable.ItemsSource = dt.DefaultView;

            //        }
            //        showData();
            //    }
            //}
        }

    }
}
