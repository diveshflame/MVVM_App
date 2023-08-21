using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_App.Models
{
    public interface IAdminBooking
    {
        public List<AdminDataGridItem> View();

        public List<AdminDataGridItem> ViewToday();
    }
}
