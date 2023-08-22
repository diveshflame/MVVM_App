using MVVM_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
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
    public partial class UserViewBookings : UserControl
    {
        public UserViewBookings()
        {
            InitializeComponent();
        }

        //public DataRowView SelectRow(int bookingId)
        //{
        //    DataRowView row = (DataRowView)UserBooking.SelectedItem;
        //    return row;
        //    AddTimingsViewModel selectD = new AddTimingsViewModel();
        //    consultType = selectD.selectdoc(doctorType1.SelectedItem.ToString());
        //    ConsultType1.ItemsSource = consultType;

        //}
    }
}
