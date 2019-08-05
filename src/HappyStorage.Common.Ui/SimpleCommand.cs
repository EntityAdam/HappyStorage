using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace HappyStorage.Common.Ui
{
    public class SimpleCommand : ISimpleCommand
    {
        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;

        public SimpleCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => canExecute == null ? true : canExecute(parameter);

        public void Execute(object parameter) => execute(parameter);

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged(EventArgs.Empty);
        }

        private void OnCanExecuteChanged(EventArgs e)
        {
            var handler = CanExecuteChanged;
            handler?.Invoke(this, e);
        }
    }
    
    public interface ISimpleCommand
    {
        event EventHandler CanExecuteChanged;

        bool CanExecute(object parameter);

        void Execute(object parameter);

        void RaiseCanExecuteChanged();
    }

}
