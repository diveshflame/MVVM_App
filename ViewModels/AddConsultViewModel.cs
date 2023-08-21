using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Ribbon;
using System.Windows.Markup;
using MVVM_App.ViewModels;
using MVVM_App.Models;
using MVVM_App.Repositories;
using System.IO.Packaging;

namespace MVVM_App.ViewModels
{
    public class AddConsultViewModel : ViewModelBase
    {
        public void Insert(string s)
        {

           ConsultModel consultModel = new ConsultModel();
            consultModel.consultName = s;
            AddRepo addRepo = new AddRepo();
            addRepo.add(s);
        }
    }
}
