using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace FeelApp.ViewModel.Commands
{
    public class DelegateCommand : ICommand
    {
        public Action<object> ExecuteFunc { get; set; }

        public Predicate<object> CanExecuteFunc { get; set; }

        public DelegateCommand()
        {
        }

        public DelegateCommand(Action<object> executeMethod)
        {
            ExecuteFunc = executeMethod;
            CanExecuteFunc = (obj) => true;
        }

        public DelegateCommand(Action<object> executeMethod, Predicate<object> canExecute)
        {
            ExecuteFunc = executeMethod;
            CanExecuteFunc = canExecute;
        }

        public event EventHandler CanExecuteChanged;
        protected void NotifyCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc(parameter);
        }

        public void Execute(object parameter)
        {
            ExecuteFunc(parameter);
        }
    }
}
