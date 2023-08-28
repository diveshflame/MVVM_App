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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Security.Principal;
using System.Threading;

namespace MVVM_App.ViewModels
{
    public class RegistrationViewModel : BaseViewModel/*, INotifyDataErrorInfo*/
    {
        private UserDetails _userDetails;
        private Dictionary<string, List<string>> _validationErrors;
        private bool _isViewVisible = true;

        public bool HasErrors => _validationErrors.Values.Any(list => list != null && list.Count > 0);
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

        public ICommand RegisterCommand { get; private set; }
        public IEnumerable<string> Genders { get; } = new List<string> { "Male", "Female", "Other" };

        

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
       

        private void RaiseErrorsChanged(string propertyName) => ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));


        public RegistrationViewModel()
        {
            UserDetails = new UserDetails();
            RegisterCommand = new RelayCommand(Validations, CanRegister);
            _validationErrors = new Dictionary<string, List<string>>();
            //UserDetails.PropertyChanged += ValidateProperty;
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
                    string connectionString = @"Server=localhost;Port=5432;User Id=postgres;Password=007;Database=WeCareDB";

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

                            Thread.CurrentPrincipal = new GenericPrincipal(
                            new GenericIdentity(UserDetails.Username), null);

                            Window1 loadLogin = new Window1();
                            loadLogin.Show();

                            IsViewVisible = false;

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

        //private void ValidateProperty(object sender, PropertyChangedEventArgs e)
        //{
        //    if (_validationErrors.ContainsKey(e.PropertyName))
        //    {
        //        _validationErrors.Remove(e.PropertyName);
        //        RaiseErrorsChanged(e.PropertyName);
        //    }

        //    var context = new ValidationContext(UserDetails) { MemberName = e.PropertyName };
        //    var validationResults = new List<ValidationResult>();
        //    Validator.TryValidateProperty(UserDetails.GetType().GetProperty(e.PropertyName).GetValue(UserDetails), context, validationResults);

        //    if (validationResults.Count > 0)
        //    {
        //        _validationErrors.Add(e.PropertyName, new List<string>());
        //        foreach (var validationResult in validationResults)
        //        {
        //            _validationErrors[e.PropertyName].Add(validationResult.ErrorMessage);
        //        }
        //        RaiseErrorsChanged(e.PropertyName);
        //    }
        //}

        //public System.Collections.IEnumerable GetErrors(string propertyName)
        //{
        //    if (string.IsNullOrEmpty(propertyName) || !_validationErrors.ContainsKey(propertyName))
        //    {
        //        return null;
        //    }
        //    return _validationErrors[propertyName];
        //}

        private void Validations()
        {
            string phoneNumber = UserDetails.PhoneNumber;
            string email = UserDetails.Email;
            string pass = UserDetails.Password;
            string phoneFormat = "^[6789]\\d{9}$"; //Phone number Regex
            string emailFormat = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; //Email regex
            Regex phonePattern = new Regex(phoneFormat);
            Regex emailPattern = new Regex(emailFormat);
            Regex password = new Regex(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\w\d\s:])([^\s]){8,16}$"); //password regex

            if (UserDetails.Username != null)
            {
                if (UserDetails.FullName != null)
                {
                    if (UserDetails.Gender != null)
                    {
                        if (UserDetails.PhoneNumber != null)
                        {
                            if (UserDetails.Email != null)
                            {
                                if (emailPattern.IsMatch(email))
                                {
                                    if (phonePattern.IsMatch(phoneNumber))
                                    {
                                        if (UserDetails.Password != null)
                                        {
                                            if (UserDetails.ConfirmPassword != null)
                                            {
                                                if (password.IsMatch(pass))
                                                {
                                                    if (UserDetails.Password != string.Empty)
                                                    {
                                                        if (UserDetails.Password == UserDetails.ConfirmPassword)
                                                        {
                                                            Register();      // Call to Insert User Data into Database
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Passwords dont match");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Password Cannot be Empty");
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Password should have 1 Uppercase/1 Special character/1 digit/7 characters");
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Enter confirm password");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Enter Password");
                                        }
                                    }

                                    else
                                    {
                                        MessageBox.Show("Invalid Phone Number");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Invalid Email Adress");
                                }

                            }
                            else
                            {
                                MessageBox.Show("Enter Email Adress");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Enter Phone Number");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Enter Your Gender");
                    }
                }
                else
                {
                    MessageBox.Show("Enter Full Name");
                }               
            }
            else { MessageBox.Show("Please enter your name"); }
        }
    }
    
}

