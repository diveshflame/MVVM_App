using MVVM_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MVVM_App.views
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Window
    {
        private SecureString enteredPassword = new SecureString();
        private bool isMasked = false;
        private string originalText;

        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void MaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isMasked)
            {
                originalText = confirmTxtbox.Text;
                confirmTxtbox.Text = MaskText(confirmTxtbox.Text);
            }
            else
            {
                confirmTxtbox.Text = originalText;
            }

            isMasked = !isMasked;
        }
        private string MaskText(string input)
        {
            // Replace each character with an asterisk
            return new string('*', input.Length);
        }

        private string UnmaskText(string input)
        {
            // Simply return the input
            return input.Replace("*", "");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
            this.Close();
        }
    }

}
