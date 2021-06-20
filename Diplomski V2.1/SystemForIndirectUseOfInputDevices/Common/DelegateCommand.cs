using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Common
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _action;
        public Func<bool> _checkCanExecute;

        public DelegateCommand(Action action, Func<bool> checkCanExecute = null)
        {
            _action = action;
            _checkCanExecute = checkCanExecute;
        }


        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }


        public void Execute(object parameter)
        {
            _action();
        }

        public bool CanExecute(object parameter)
        {
            if (_checkCanExecute != null) return _checkCanExecute();
            return true;
        }
        public event EventHandler CanExecuteChanged;
    }  
    
    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> _action;
        public Func<bool> _checkCanExecute;

        public DelegateCommand(Action<T> action, Func<bool> checkCanExecute = null)
        {
            _action = action;
            _checkCanExecute = checkCanExecute;
        }


        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }


        public void Execute(object parameter)
        {
            _action((T)parameter);
        }

        public bool CanExecute(object parameter)
        {
            if (_checkCanExecute != null) return _checkCanExecute();
            return true;
        }
        public event EventHandler CanExecuteChanged;
    }
}
