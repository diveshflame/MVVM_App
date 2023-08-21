using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Ribbon;
using System.Windows.Markup;
using WpfApp4.Models;
using WpfApp4.Repositories;

namespace WpfApp4.ViewModels
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
