using System;
using System.Windows.Input;

namespace RedSheeps.Input
{
    public class Command : ICommand
    {
        private static readonly Func<object, bool> ReturnTrue = _ => true;

        private readonly Action<object> _execute;

        private readonly Func<object, bool> _canExecute;

        public Command(Action<object> execute) : this(execute, ReturnTrue)
        {
        }

        public Command(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public Command(Action execute) : this(_ => execute(), ReturnTrue)
        {
        }

        public Command(Action execute, Func<bool> canExecute) : this(_ => execute(), _ => canExecute())
        {
        }

        public bool CanExecute(object parameter) => _canExecute(parameter);

        public void Execute(object parameter) => _execute(parameter);

        public void NotifyCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public event EventHandler CanExecuteChanged;
    }

    public class Command<T> : Command
    {
        public Command(Action<T> execute) 
            : base(param => execute((T)param)) 
        {
        }
        public Command(Action<T> execute, Func<T, bool> canExecute)
            : base(param => execute((T)param), param => canExecute((T)param))
        {
        }
    }
}
