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
                    "BOOKING_TABLE.DOCTOR_ID = DOCTOR_TABLE.DOCTOR_ID INNER JOIN CONSULTANT_TYPE ON DOCTOR_TABLE.CONSULTANT_ID = CONSULTANT_TYPE.CONSULTANT_ID WHERE DELETED_TIMESTAMP IS NULL AND DATE(STARTTIME)>=CURRENT_DATE ORDER BY STARTTIME";
                NpgsqlCommand cmd = new NpgsqlCommand(view, conn);
                using(var reader = cmd.ExecuteReader()) 
                {
                    while(reader.Read())
                    {
                        item = new DataGridItem()
                        {
                            Name = reader.GetString("NAME"),
                            DoctorName = reader.GetString("DOCTOR_NAME"),
                            StartTime = reader.GetDateTime("STARTTIME").ToString("dd/MM/yyyy HH:mm"),
                            EndTime = reader.GetDateTime("ENDTIME").ToString("dd/MM/yyyy HH:mm"),
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
                    "BOOKING_TABLE.DOCTOR_ID = DOCTOR_TABLE.DOCTOR_ID INNER JOIN CONSULTANT_TYPE ON DOCTOR_TABLE.CONSULTANT_ID = CONSULTANT_TYPE.CONSULTANT_ID AND DELETED_TIMESTAMP IS NULL ORDER BY STARTTIME ";
                NpgsqlCommand cmd = new NpgsqlCommand(view, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        item = new DataGridItem()
                        {
                            Name = reader.GetString("NAME"),
                            DoctorName = reader.GetString("DOCTOR_NAME"),
                            StartTime = reader.GetDateTime("STARTTIME").ToString("dd/MM/yyyy HH:mm"),
                            EndTime = reader.GetDateTime("ENDTIME").ToString("dd/MM/yyyy HH:mm"),
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
                    "BOOKING_TABLE.DOCTOR_ID = DOCTOR_TABLE.DOCTOR_ID INNER JOIN CONSULTANT_TYPE ON DOCTOR_TABLE.CONSULTANT_ID = CONSULTANT_TYPE.CONSULTANT_ID WHERE DELETED_TIMESTAMP IS NULL AND DATE(STARTTIME)=CURRENT_DATE ORDER BY STARTTIME";
                NpgsqlCommand cmd = new NpgsqlCommand(view, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        item = new DataGridItem()
                        {
                            
                            Name = reader.GetString("NAME"),
                            DoctorName = reader.GetString("DOCTOR_NAME"),
                            StartTime = reader.GetDateTime("STARTTIME").ToString("dd/MM/yyyy HH:mm"),
                            EndTime = reader.GetDateTime("ENDTIME").ToString("dd/MM/yyyy HH:mm"),
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
                                        WHERE ACTIVE_SESSION = 1) ORDER BY STARTTIME";
                NpgsqlCommand cmd = new NpgsqlCommand(View, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        item = new DataGridItem()
                        {
                            Booking_Id = reader.GetInt32("BOOKING_ID"),
                            Doctor_Id=reader.GetInt32("DOCTOR_ID"),
                            DoctorName = reader.GetString("DOCTOR_NAME"),
                            StartTime = reader.GetDateTime("STARTTIME").ToString("dd/MM/yyyy HH:mm"),
                            EndTime = reader.GetDateTime("ENDTIME").ToString("dd/MM/yyyy HH:mm"),
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
                               WHERE  DELETED_TIMESTAMP IS NULL AND DATE(STARTTIME)< CURRENT_DATE AND  BOOKING_TABLE.USERID IN
                                    (SELECT USERID
                                        FROM USERDETAILS
                                        WHERE ACTIVE_SESSION = 1) ORDER BY STARTTIME";
                NpgsqlCommand cmd = new NpgsqlCommand(View, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        item = new DataGridItem()
                        {
                            Booking_Id = reader.GetInt32("BOOKING_ID"),
                            Doctor_Id = reader.GetInt32("DOCTOR_ID"),
                            DoctorName = reader.GetString("DOCTOR_NAME"),
                            StartTime = reader.GetDateTime("STARTTIME").ToString("dd/MM/yyyy HH:mm"),
                            EndTime = reader.GetDateTime("ENDTIME").ToString("dd/MM/yyyy HH:mm"),
                            Consultant_Desc = reader.GetString("CONSULTANT_DESC"),
                        };
                        NewList.Add(item);
                    }
                }

            }
            return NewList;
        }

        public List<DataGridItem> ViewUsersTodayBooking()
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
                               WHERE DELETED_TIMESTAMP IS NULL AND DATE(STARTTIME)= CURRENT_DATE AND BOOKING_TABLE.USERID IN
                                    (SELECT USERID
                                        FROM USERDETAILS
                                        WHERE ACTIVE_SESSION = 1) ORDER BY STARTTIME";
                NpgsqlCommand cmd = new NpgsqlCommand(View, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        item = new DataGridItem()
                        {
                            Booking_Id = reader.GetInt32("BOOKING_ID"),
                            Doctor_Id = reader.GetInt32("DOCTOR_ID"),
                            DoctorName = reader.GetString("DOCTOR_NAME"),
                            StartTime = reader.GetDateTime("STARTTIME").ToString("dd/MM/yyyy HH:mm"),
                            EndTime = reader.GetDateTime("ENDTIME").ToString("dd/MM/yyyy HH:mm"),
                            Consultant_Desc = reader.GetString("CONSULTANT_DESC"),
                        };
                        NewList.Add(item);
                    }
                }

            }
            return NewList;
        }
        public bool DeleteUserBooking(int BookingId, int DoctorId, DateTime startTime, DateTime endTime)
        {
            bool isvalid = false;
            int getbookingid = BookingId;
            int getDoctorid= DoctorId;
            DateTime getstarttime = startTime;
            DateTime getendtime = endTime;
            DateTime cancelDate = getstarttime;
            var Differencedate = cancelDate - DateTime.Now;
            if (MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (Differencedate.Days < 2)
                {
                    MessageBox.Show("You cannnot Cancel the appointment 48 hours prior to the appointment.Please Contact the doctor's office.", "Detail");
                }
                else
                {
                    DateTime dateTime = DateTime.Now;
                    using (NpgsqlConnection conn = GetConnection())
                    {
                        conn.Open();
                        string View = "UPDATE Booking_Table SET Deleted_TimeStamp = @nowdate::timestamp WHERE Booking_Id = @id;";
                        NpgsqlCommand cmd = new NpgsqlCommand(View, conn);
                        cmd.Parameters.AddWithValue("@nowdate", dateTime);
                        cmd.Parameters.AddWithValue("@id", getbookingid);
                        cmd.ExecuteNonQuery();
                        string delete = "insert into Doctor_Availability(doctor_id,available_starttime,available_endtime) values (@doctor,@starttime,@endtime)";
                        NpgsqlCommand cmd2 = new NpgsqlCommand(delete, conn);
                        cmd2.Parameters.AddWithValue("@doctor", getDoctorid);
                        cmd2.Parameters.AddWithValue("@starttime", getstarttime);
                        cmd2.Parameters.AddWithValue("@endtime", getendtime);
                        cmd2.ExecuteNonQuery();
                    }
                    isvalid = true;
                }
            }
            return isvalid;
        }


    }

    }
