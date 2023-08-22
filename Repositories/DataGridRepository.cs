using MVVM_App.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_App.Repositories;

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

    }
}
