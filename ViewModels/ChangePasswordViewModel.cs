using MVVM_App.Models;
using MVVM_App.views;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace MVVM_App.ViewModels
{
    public class ChangePasswordViewModel : BaseViewModel
    {

        private UserDetails _userDetails;

        public ICommand ChangePasswordCommand { get; private set; }

        private bool _isViewVisible = true;
        public bool IsViewVisible { get => _isViewVisible; set { _isViewVisible = value; OnPropertyChanged(nameof(IsViewVisible)); } }


        public UserDetails UserDetails
        {
            get { return _userDetails; }
            set
            {
                _userDetails = value;
                OnPropertyChanged();
            }
        }

        public ChangePasswordViewModel()
        {
            UserDetails=new UserDetails();
            ChangePasswordCommand = new RelayCommand(Update, Canupdate);
        }
        private bool Canupdate()
        {
            return true;
        }

        private void Update()
        {
            if(Canupdate())
            {
                try
                {
                    string connectionString = @"Server=localhost;Port=5432;User Id=postgres;Password=pass;Database=postgres";

                    using (var connection = new NpgsqlConnection(connectionString))
                    {

                        connection.Open();

                        using (var cmd = new NpgsqlCommand())
                        {
                            cmd.Connection = connection;
                            cmd.CommandText = "update userDetails set password=@password where username=@username";
                            cmd.Parameters.Add(new NpgsqlParameter("@username", UserDetails.ChangeUsername));
                            cmd.Parameters.Add(new NpgsqlParameter("@password", UserDetails.ChangePassword));
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Password changed successfully");

                            Window1 loginView = new Window1();
                            loginView.Show();
                           
                            IsViewVisible = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
        }
    }

    
}
