using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_App.Models
{
    public interface IAddDocRepo
    {
        public List<string> get();

        public void add(string id);
        public List<TimeSpan> startT();
        public List<TimeSpan> EndT();

        public List<string> getco();
        public bool UpdateDoc(int Doc_id, int Consult_id);
        int GetDoctorId(string selectedDoctorName);
        int GetConsultantId(string selectedConsultationtype);
    }
}
