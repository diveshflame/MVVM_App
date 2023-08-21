using System;
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
    public class AddTimings : ViewModelBase
    {
        public IAddDocRepo repocall = new AddRepo();

        public List<string> DoctorNames { get; set; }
        public List<TimeSpan> StartTime { get; set; }
        public List<TimeSpan> EndTime { get; set; }
        public AddTimings()
        {
           DoctorNames = repocall.get();

           StartTime = repocall.startT();

           EndTime = repocall.EndT();  
          
        }
        
    }
}
