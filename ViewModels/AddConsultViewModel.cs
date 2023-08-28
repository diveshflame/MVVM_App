using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Ribbon;
using System.Windows.Markup;
using MVVM_App.ViewModels;
using MVVM_App.Models;
using MVVM_App.Repositories;
using System.IO.Packaging;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;


namespace MVVM_App.ViewModels
{
    public class AddConsultViewModel : ViewModelBase , INotifyPropertyChanged
    {
        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged(nameof(Text));
                }
            }
            
        }
        public ICommand AddDepartment { get; }
        public IAddDocRepo addDocRepo;

        public AddConsultViewModel()
        {
            addDocRepo = new AddRepo();
            AddDepartment = new ViewModelCommand(ExecuteAddDepartment);
        }
        public void ExecuteAddDepartment(object obj)
        {
           

            if (string.IsNullOrWhiteSpace(Text))
            {
                MessageBox.Show("Please enter a Valid Department Type!!", "Error Message");
                return;
            }

            else if(addDocRepo!=null) 
            {
                addDocRepo.add(Text);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

     
    }
}
