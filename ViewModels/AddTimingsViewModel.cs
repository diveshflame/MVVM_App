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
using static System.Net.Mime.MediaTypeNames;

namespace MVVM_App.ViewModels
{
    public class AddTimingsViewModel : ViewModelBase
    {
        public IAddDocRepo repocall = new AddRepo();
        int a = 0;
        public List<string> DoctorNames { get; set; }
        public List<string> Consult { get; set; }
        public List<TimeSpan> StartTime { get; set; }
        public List<TimeSpan> EndTime { get; set; }
        public AddTimingsViewModel()
        {
            DoctorNames = repocall.get();

            StartTime = repocall.startT();

            EndTime = repocall.EndT();



        }
        public List<string> selectdoc(string text)
        {
            AddRepo addRepo = new AddRepo();

            Consult = addRepo.selectionchangedoc1(text);
            return Consult;


        }

        public int Selectcon(DateTime dat1, DateTime dat2, string s, DateTime startDate, DateTime endDate)
        {
            AddRepo addRepo = new AddRepo();
           a= addRepo.selectionconchanged(dat1, dat2, s, startDate, endDate);
            return a;
        }

        public void check(DateTime dat1, DateTime dat2, string s, DateTime startDate, DateTime endDate, string FromTime, string EndTime,int T)
        {
            AddRepo addRepo = new AddRepo();
            addRepo.ischecked(dat1, dat2, s, startDate, endDate, FromTime, EndTime,T);

        }
    }
}
