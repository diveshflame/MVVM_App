using MVVM_App.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace MVVM_App.ViewModels
{
    public class ChangePasswordViewModel : BaseViewModel
    {
        private UserDetails _userDetails;

        public ICommand ChangePasswordCommand { get; private set; }

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
