using System;
using System.Windows.Input;

namespace DragAndDrop.Utils
{
    public class Command : ICommand
    {
        private Action _action;
        private Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;


        public Command(Action action, Func<bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }


        public bool CanExecute(object parameter) => _canExecute.Invoke();

        public void Execute(object parameter) => _action();
    }


    public class Command<T> : ICommand
    {
        private Action<T> _action;
        private Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public Command(Action<T> action, Func<bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute.Invoke();

        public void Execute(object parameter)
        {
            if (parameter is T t)
                _action.Invoke(t);
        }
    }
}
