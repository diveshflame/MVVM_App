using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_App.ViewModels
{
    internal class ViewModelCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canexecuteAction;

        public ViewModelCommand(Action<object> execute)
        {
            _execute = execute;
            _canexecuteAction = null;
        }

        public ViewModelCommand(Action<object> execute, Predicate<object> canexecuteAction)
        {
            _execute = execute;
            _canexecuteAction = canexecuteAction;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canexecuteAction == null ? true: _canexecuteAction(parameter);   
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
