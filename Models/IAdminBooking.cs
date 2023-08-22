using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_App.Models
{
    public interface IAdminBooking
    {
        public List<DataGridItem> View();

        public List<DataGridItem> ViewToday();
        public List<DataGridItem> ViewHistory();
    }
}
