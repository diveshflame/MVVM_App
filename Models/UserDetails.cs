using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace MVVM_App.Models
{
    public class UserDetails : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _username;
        private string _fullName;
        private int _age;
        private string _gender;
        private string _email;
        private string _phoneNumber;
        private string _password;
        private string _confirmPassword;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [Required(ErrorMessage = "Username is required.")]
        public string Username
        {
            get { return _username; }
            set { _username = value;
                OnPropertyChanged();
            }
            

        }

        [Required(ErrorMessage = "Full Name is required.")]
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value;
                OnPropertyChanged();
            }
        }

        [Required(ErrorMessage = "Age is required.")]
        [Range(18, 120, ErrorMessage = "Age must be between 18 and 120.")]
        public int Age
        {
            get { return _age; }
            set { _age = value;
                OnPropertyChanged();
            }
        }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender
        {
            get { return _gender; }
            set { _gender = value;
                OnPropertyChanged();
            }
        }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email
        {
            get { return _email; }
            set { _email = value;
                OnPropertyChanged();
            }
        }

        [Required(ErrorMessage = "Phone Number is required.")]
        [Phone(ErrorMessage = "Invalid Phone Number.")]
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        [Required(ErrorMessage = "Password is required.")]
        public string Password
        {
            get { return _password; }
            set { _password = value;
                OnPropertyChanged();
            }
        }

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
            }
        }


    }
}

