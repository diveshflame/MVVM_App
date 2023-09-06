using System;
using System.Collections.Generic;
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

        public List<DataGridItem> Search(string searchContent);
        public List<DataGridItem> ViewUsersTodayBooking();
        public List<DataGridItem> ViewUsersHistory();

        bool DeleteUserBooking(int bookingId, int doctorId, DateTime startTime, DateTime endTime);
    }
}
