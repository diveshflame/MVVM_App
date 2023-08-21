﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Ribbon;
using System.Windows.Markup;
using MVVM_App.Models;
using MVVM_App.Repositories;

namespace MVVM_App.ViewModels
{
    public class AddDoctor : ViewModelBase
    {
        public IAddDocRepo repocall = new AddRepo();

        public List<string> DoctorNames { get; set; }
        public AddDoctor()
        {
           DoctorNames = repocall.get();
          
        }

    }
}
