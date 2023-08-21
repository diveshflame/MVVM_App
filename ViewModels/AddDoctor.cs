using MVVM_App.Models;
using MVVM_App.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_App.ViewModels
{
    public class AddDoctor : ViewModelBase
    {
        public List<string>consuld=new List<string>(); 
        public IAddDocRepo repocall = new AddRepo();
        public AddDoctor()
        {

            consuld = repocall.getco();
        }
    }
}
