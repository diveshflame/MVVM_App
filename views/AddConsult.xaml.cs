using MVVM_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVM_App.views
{
    /// <summary>
    /// Interaction logic for AddConsult.xaml
    /// </summary>
    public partial class AddConsult : UserControl
    {
        public AddConsult()
        {
            InitializeComponent();
        }

        
        private void TextBox_Consultation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            // If the input contains characters other than letters, block it.
            if (regex.IsMatch(e.Text))
            {
                e.Handled = true;
            }
        }

    }
}
