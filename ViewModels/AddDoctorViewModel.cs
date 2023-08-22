using MVVM_App.Models;
using MVVM_App.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_App.ViewModels
{
    public class AddDoctorViewModel : ViewModelBase
    {
        public List<string> consuld { get; set; }
        public IAddDocRepo repocall = new AddRepo();
        public AddDoctorViewModel()
        {

            consuld = repocall.getco();
        }

        public void Insert(string text, string v)
        {

            ConsultModel consultModel = new ConsultModel();
            consultModel.docname=text;
            consultModel.consulD = v;
            AddRepo addRepo = new AddRepo();
            addRepo.addDoctor(text,v);
        }
    }
}
