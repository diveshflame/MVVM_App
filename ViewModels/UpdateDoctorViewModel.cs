using MVVM_App.Models;
using MVVM_App.Repositories;
using MVVM_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_App.ViewModels
{
   public class UpdateDoctorViewModel : ViewModelBase
    {

        //public string  SelectedDocType { get; set; }
        private List<string> doctorNames;
        private List<string> consult;
        public List<string> DoctorNames { get => doctorNames; set { doctorNames = value; OnPropertyChanged(nameof(DoctorNames)); } }

        public List<string> Consult { get => consult; set { consult = value; OnPropertyChanged(nameof(Consult)); } }

        //Commands
        public ICommand UpdateDoctor;
        public IAddDocRepo addDocRepo;
        //Constructor
        public UpdateDoctorViewModel()
        {
            //addDocRepo = new ();
            //    ViewUserBookings = new ViewModelCommand(ExecuteViewUserBookings);
        }

        public void updatedoc(string text, string v)
        {

            ConsultModel consultModel = new ConsultModel();
            consultModel.docname = text;
            consultModel.consulD = v;
            AddRepo addRepo = new AddRepo();
            addRepo.addDoctor(text, v);
        }

    }
}


//public IAddDocRepo repocall = new AddRepo();
//public AddDoctorViewModel()
//{
//    consuld = repocall.getco();
//}

//public void Insert(string text, string v)
//{

//    ConsultModel consultModel = new ConsultModel();
//    consultModel.docname = text;
//    consultModel.consulD = v;
//    AddRepo addRepo = new AddRepo();
//    addRepo.addDoctor(text, v);
//}


//public ICommand DeleteUserBookings { get; }

//private IAdminBooking adminbooking;



////constructor
//public UserViewBookingsModel()
//{
//    adminbooking = new DataGridRepository();
//    ViewUserBookings = new ViewModelCommand(ExecuteViewUserBookings);
//    ViewUserTodayBooking = new ViewModelCommand(ExecuteViewUserTodayBooking);
//    ViewUserBookingHistory = new ViewModelCommand(ExecuteViewUserBookingHistory);
//    IsDataGridVisible = true;
//    IsDataGridVisible2 = false;
//    LoadData();
//}



//private void ExecuteViewUserBookingHistory(object obj)
//{
//    IsDataGridVisible = false;
//    IsDataGridVisible2 = true;
//    UserViewBookingsModel view = new UserViewBookingsModel();
//    UserNewDatagridItems = new ObservableCollection<DataGridItem>();
//    var dataGridItem = adminbooking.ViewUsersHistory();
//    foreach (var item in dataGridItem)
//    {
//        UserNewDatagridItems.Add(item);
//    }
//}
