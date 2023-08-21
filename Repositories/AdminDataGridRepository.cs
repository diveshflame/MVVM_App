using MVVM_App.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp4.Repositories;

namespace MVVM_App.Repositories
{
    public class AdminDataGridRepository : RepositoryBase, IAdminBooking
    {
        public List<AdminDataGridItem> View()
        {
            List<AdminDataGridItem> NewList =new List<AdminDataGridItem>();
            AdminDataGridItem item = null; ;
            using(NpgsqlConnection conn= GetConnection())
            {
                string view = "SELECT NAME,DOCTOR_NAME,STARTTIME, ENDTIME, CONSULTANT_DESC FROM USERDETAILS INNER JOIN BOOKING_TABLE ON USERDETAILS.USERID = BOOKING_TABLE.USERID INNER JOIN DOCTOR_TABLE ON " +
                    "BOOKING_TABLE.DOCTOR_ID = DOCTOR_TABLE.DOCTOR_ID INNER JOIN CONSULTANT_TYPE ON DOCTOR_TABLE.CONSULTANT_ID = CONSULTANT_TYPE.CONSULTANT_ID WHERE DELETED_TIMESTAMP IS NULL";
                NpgsqlCommand cmd = new NpgsqlCommand(view, conn);
                using(var reader = cmd.ExecuteReader()) 
                {
                    while(reader.Read())
                    {
                        item = new AdminDataGridItem()
                        {
                            Name = reader.GetString("USERNAME"),
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

        public List<AdminDataGridItem> ViewToday()
        {
            throw new NotImplementedException();
        }
    }
}
