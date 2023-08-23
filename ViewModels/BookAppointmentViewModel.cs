using MVVM_App.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_App.ViewModels
{
    public class BookAppointmentViewModel: ViewModelBase
    {
       public List<TimeSpan> slotlist =new List<TimeSpan>();
       public List<TimeSpan> slotlist2 = new List<TimeSpan>();
        public List<string> Consult { get; set; }

        public List<string> Doctor { get; set; }
        BookRepo bookRepo = new BookRepo();

        public BookAppointmentViewModel() 
        
        { 
        
        BookRepo bookRepo = new BookRepo();
        Consult = bookRepo.bookgetco();   
        
        }

        public List<string> consulchange(string s)
        {
            
            Doctor = bookRepo.bookgetDoc(s);
            return Doctor;
        }

        public void docSelChanged(string doc, DateTime selectedDate)
        {
         
            slotlist.Clear();
            slotlist2.Clear();  
            bookRepo.BookGetTime(doc, selectedDate);
            slotlist= bookRepo.slotlist;
            slotlist2= bookRepo.slotlist2;  
        }

        public void Add(string selectedDep, DateTime selectedDate, string doc, DateTime d1, DateTime d2)
        {
            bookRepo.AddBook(selectedDep, selectedDate, doc, d1, d2);
        }
    }
}
