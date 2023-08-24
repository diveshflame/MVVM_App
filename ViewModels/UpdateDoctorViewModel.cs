using MVVM_App.Models;
using MVVM_App.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_App.ViewModels
{
   public class UpdateDoctorViewModel : ViewModelBase
    {
       
        public string  SelectedDocType { get; set; }
        public List<string> DoctorNames { get; set; }
        public List<string> Consult { get; set; }

        public IAddDocRepo aa;
        public UpdateDoctorViewModel()
        {
            
        }

        public void Insert(string text, string v)
        {

            ConsultModel consultModel = new ConsultModel();
            consultModel.docname = text;
            consultModel.consulD = v;
            AddRepo addRepo = new AddRepo();
            addRepo.addDoctor(text, v);
        }

    }
}
