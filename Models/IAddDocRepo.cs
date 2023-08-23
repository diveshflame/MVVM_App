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

        public List<string> UpdateDoc();
    }
}
