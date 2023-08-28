using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_App.Services
{
    public interface INavigationService
    {
        void NavigateToRegistration();
        void NavigateToLogin();
        void CloseCurrentWindow();
    }
}
