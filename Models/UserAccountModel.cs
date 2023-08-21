using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_App.Models
{
   public class UserAccountModel
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }

        public byte[] Profilepic { get; set; }    
    }
}
