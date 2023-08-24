using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_App.Models
{
    public class UserModel
    {
        public string Id { get; set; }  
        public string UserName { get; set; }    

        public string Password { get; set; }    

        public string Email { get; set; }

        public bool SuperUser { get; set; }

    }
}
