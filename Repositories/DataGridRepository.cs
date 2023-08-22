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
using System.Windows.Controls;

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
        public void DeleteUserBooking(DataRowView a)
        {
            string getbookingid = a["Booking_Id"].ToString();
            DateTime cancelDate = (DateTime)a["StartTime"];
            string docid = a["Doctor_Id"].ToString();
            DateTime starttime = DateTime.Parse(a["StartTime"].ToString());
            DateTime endtime = DateTime.Parse(a["EndTime"].ToString());
            var Differencedate = cancelDate - DateTime.Now;
            if (Differencedate.Days < 2)
            {
                //btnDelete.IsEnabled = false;
                MessageBox.Show("You cannnot Cancel the appointment 48 hours prior to the appointment.Please Contact the doctor's office.", "Detail");
            }
            else
            {
                a.Delete();
                DateTime dateTime = DateTime.Now;

                using (NpgsqlConnection conn = GetConnection())
                {
                    conn.Open();
                    string View = "update Booking_Table set Daleted_TimeStamp=" + "'" + dateTime.ToString("yyyy-MM-dd HH:mm:ss") + "'" + "where Booking_Id=" + getbookingid;
                    NpgsqlCommand cmd = new NpgsqlCommand(View, conn);
                    //DataTable dt = new DataTable();
                    // cmd.ExecuteNonQuery();
                    // dt.Load(cmd.ExecuteReader());
                    string delete = "insert into DoctorAvailability values (@doctor,@starttime,@endtime)";
                    NpgsqlCommand cmd2 = new NpgsqlCommand(delete, conn);
                    cmd2.Parameters.AddWithValue("@doctor", docid);
                    cmd2.Parameters.AddWithValue("@starttime", starttime);
                    cmd2.Parameters.AddWithValue("@endtime", endtime);
                    cmd2.ExecuteNonQuery();
                }
            }
        }


    }

    }
