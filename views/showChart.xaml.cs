﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using MVVM_App.ViewModels;


namespace MVVM_App.views
{
    /// <summary>
    /// Interaction logic for showChart.xaml
    /// </summary>
    public partial class showChart : UserControl
    {
        public showChart()
        {
            InitializeComponent();
            DataContext = new chartViewModel();
        }
    }
}