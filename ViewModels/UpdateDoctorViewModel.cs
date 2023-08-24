using Microsoft.Identity.Client;
using MVVM_App.Models;
using MVVM_App.Repositories;
using MVVM_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace MVVM_App.ViewModels
{
   public class UpdateDoctorViewModel : ViewModelBase
    {
        //Fileds
        private List<string> doctorNames;
        private List<string> consult;
        private string selectedDoctorName;
        private string selectedConsultationtype;
        public List<string> DoctorNames { get => doctorNames; set { doctorNames = value; OnPropertyChanged(nameof(DoctorNames)); } }

        public List<string> Consult { get => consult; set { consult = value; OnPropertyChanged(nameof(Consult)); } }

        public string SelectedDoctorName { get => selectedDoctorName; set { selectedDoctorName = value; OnPropertyChanged(nameof(SelectedDoctorName)); } }
        public string SelectedConsultationtype { get => selectedConsultationtype; set { selectedConsultationtype = value; OnPropertyChanged(nameof(SelectedConsultationtype)); } }

        //Commands
        public ICommand UpdateDoctor { get; }
        public IAddDocRepo addDocRepo;

        //Constructor
        public UpdateDoctorViewModel()
        {
            addDocRepo = new AddRepo();
            consult = addDocRepo.getco();
            doctorNames = addDocRepo.get();
            UpdateDoctor = new ViewModelCommand(ExecuteUpdateDoctor);
        }

        private void ExecuteUpdateDoctor(object obj)
        {
            if ((string.IsNullOrWhiteSpace(SelectedDoctorName) && string.IsNullOrWhiteSpace(SelectedConsultationtype)))
            {
                MessageBox.Show("Please Select a Valid Value", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                SelectedConsultationtype = null;
                SelectedDoctorName = null;
            }
            else if(string.IsNullOrWhiteSpace(SelectedDoctorName))
            {
                MessageBox.Show("Doctor is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                SelectedConsultationtype = null;
                SelectedDoctorName = null;
            }
            else if((string.IsNullOrWhiteSpace(SelectedConsultationtype)))
            {
                MessageBox.Show("Department is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                SelectedConsultationtype = null;
                SelectedDoctorName = null;
            }
        
            else
            {
                int getDoctorId = addDocRepo.GetDoctorId(SelectedDoctorName);
                int getConsultantId = addDocRepo.GetConsultantId(SelectedConsultationtype);
                bool IsValid = addDocRepo.UpdateDoc(getDoctorId, getConsultantId);
                if (IsValid)
                {
                    SelectedConsultationtype = null;
                    SelectedDoctorName = null;
                }
            }
        }
    }
}





