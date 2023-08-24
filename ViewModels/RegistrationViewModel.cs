using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Npgsql;
using MVVM_App.Models;
using System.Windows;
using MVVM_App.views;

namespace MVVM_App.ViewModels
{
    public class RegistrationViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        private UserDetails _userDetails;
        private Dictionary<string, List<string>> _validationErrors;

        public bool HasErrors => _validationErrors.Values.Any(list => list != null && list.Count > 0);


        public UserDetails UserDetails
        {
            get { return _userDetails; }
            set
            {
                _userDetails = value;
                OnPropertyChanged();
            }
        }

        public ICommand RegisterCommand { get; private set; }
        public IEnumerable<string> Genders { get; } = new List<string> { "Male", "Female", "Other" };

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private void RaiseErrorsChanged(string propertyName) => ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));


        public RegistrationViewModel()
        {
            UserDetails = new UserDetails();
            RegisterCommand = new RelayCommand(Register, CanRegister);
            _validationErrors = new Dictionary<string, List<string>>();
            UserDetails.PropertyChanged += ValidateProperty;
        }

        private bool CanRegister()
        {
            return !HasErrors;
        }

        private void Register()
        {
            // Implement registration logic here
            // Insert UserDetails into the database
            if (CanRegister())
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
                            cmd.CommandText = "INSERT INTO userdetails (username, name, age, gender, email, phone_number, password) " +
                                              "VALUES (@username, @name, @age, @gender, @email, @phone_number, @password)";

                            cmd.Parameters.AddWithValue("username", UserDetails.Username);
                            cmd.Parameters.AddWithValue("name", UserDetails.FullName);
                            cmd.Parameters.AddWithValue("age", UserDetails.Age);
                            cmd.Parameters.AddWithValue("gender", UserDetails.Gender);
                            cmd.Parameters.AddWithValue("email", UserDetails.Email);
                            cmd.Parameters.AddWithValue("phone_number", UserDetails.PhoneNumber);
                            cmd.Parameters.AddWithValue("password", UserDetails.Password);

                            cmd.ExecuteNonQuery();
                            Window1 loadLogin = new Window1();
                            loadLogin.Show();  
                        }
                    }
                    
                    // Optionally, you can show a success message or navigate to another page
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., database errors) and show an error message
                    MessageBox.Show("An error occured :",ex.Message);
                }
            }
        }

        private void ValidateProperty(object sender, PropertyChangedEventArgs e)
        {
            if (_validationErrors.ContainsKey(e.PropertyName))
            {
                _validationErrors.Remove(e.PropertyName);
                RaiseErrorsChanged(e.PropertyName);
            }

            var context = new ValidationContext(UserDetails) { MemberName = e.PropertyName };
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(UserDetails.GetType().GetProperty(e.PropertyName).GetValue(UserDetails), context, validationResults);

            if (validationResults.Count > 0)
            {
                _validationErrors.Add(e.PropertyName, new List<string>());
                foreach (var validationResult in validationResults)
                {
                    _validationErrors[e.PropertyName].Add(validationResult.ErrorMessage);
                }
                RaiseErrorsChanged(e.PropertyName);
            }
        }

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !_validationErrors.ContainsKey(propertyName))
            {
                return null;
            }
            return _validationErrors[propertyName];
        }
    }
}
