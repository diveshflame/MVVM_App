using MVVM_App.Models;
using MVVM_App.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace MVVM_App.ViewModels
{
    public class AddDoctorViewModel : ViewModelBase,INotifyPropertyChanged
    {
        private List<string> consult;
        private string selectedConsultationtype;
        public List<string> Consult { get => consult; set { consult = value; OnPropertyChanged(nameof(Consult)); } }
        public string SelectedConsultationtype { get => selectedConsultationtype; set { selectedConsultationtype = value; OnPropertyChanged(nameof(SelectedConsultationtype)); } }
        public IAddDocRepo repocall = new AddRepo();
        public ICommand AddDoctor { get; }
        public AddDoctorViewModel()
        {
            consult = repocall.getCo();
            AddDoctor = new ViewModelCommand(ExecuteAddDoctor);
        }
        private string DocName;
        public string DocNameChange
        {
            get { return DocName; }
            set
            {
                if (DocName != value)
                {
                    DocName = value;
                    OnPropertyChanged(nameof(Text));
                }
            }

        }

        AddRepo addRepo = new AddRepo();
        private void ExecuteAddDoctor(object obj)
        {
            addRepo.addDoctor(DocNameChange,selectedConsultationtype);
        }

  

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
