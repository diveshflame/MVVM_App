using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MVVM_App.views;

namespace MVVM_App.views
{
    /// <summary>
    /// Interaction logic for userDashboard.xaml
    /// </summary>
    public partial class userDashboard : Window
    {
        public userDashboard()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }
        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
                this.WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState |= WindowState.Minimized;
        }

        private void UserProfileButton_Click(object sender, RoutedEventArgs e)
        {
            userDropdownPopup.IsOpen = !userDropdownPopup.IsOpen;
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement your settings logic here
            userDropdownPopup.IsOpen = false;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement your logout logic here
            userDropdownPopup.IsOpen = false;
            Window1 loginView = new Window1();
            loginView.Show();
            this.Close();
        }
    }
}
