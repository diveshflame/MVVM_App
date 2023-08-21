using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_App.Models
{
    public class AdminDataGridItem
    {
        public string Name { get; set; }
        public string DoctorName { get; set; }
        public string Consultant_Desc { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
