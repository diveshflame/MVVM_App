using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_App.Models
{
    public interface IAdminBooking
    {
        //Admin User Bookings
        public List<DataGridItem> View();

        public List<DataGridItem> ViewToday();
        public List<DataGridItem> ViewHistory();
        
        //User View Bookings
        public List<DataGridItem> ViewUserBookings();
        public List<DataGridItem> ViewUsersTodayBooking();
        public List<DataGridItem> ViewUsersHistory();

        public bool  DeleteUserBooking(int BookingId, int DoctorId, DateTime startTime, DateTime endTime);

    }
}
