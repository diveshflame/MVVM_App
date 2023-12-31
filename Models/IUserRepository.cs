﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_App.Models
{
    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);    
        void Add(UserModel userModel);
        UserModel GetByUsername(string username);   
        UserModel GetById(int id);
        bool IsUserSuperUser(NetworkCredential credential);

    }
}
