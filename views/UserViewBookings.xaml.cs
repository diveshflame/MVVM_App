using MVVM_App.Models;
using MVVM_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVM_App.views
{
    /// <summary>
    /// Interaction logic for UserViewBookings.xaml
    /// </summary>
    public partial class UserViewBookings : System.Windows.Controls.UserControl
    {
        public UserViewBookings()
        {
            InitializeComponent();
        }

        private void Deleteappoitment(object sender, RoutedEventArgs e)
        {

            UserViewBookingsModel add = new UserViewBookingsModel();
            DataGridItem row = UserBooking.SelectedItem as DataGridItem;

           // DataRowView row = UserBooking.SelectedItem as DataRowView;
            int BookingId = row.Booking_Id;
            DateTime starttime = row.StartTime;
            DateTime endtime = row.EndTime;
            int docid = row.Doctor_Id;
            add.DeleteAppointment(BookingId,docid,starttime,endtime);
           


        }
    }
}
