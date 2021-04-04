using System;
using System.Windows.Input;

namespace FamilyTree.Core.Commands
{
    public abstract class CommandBase<T> : ICommand
    {
        public virtual event EventHandler CanExecuteChanged;

        protected T ViewModel { get; set; }

        public CommandBase(T ViewModel)
        {
            this.ViewModel = ViewModel;
        }

        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);

        public void RaiseCanExecuteChanged(object sender, EventArgs eventArgs)
        {
            CanExecuteChanged?.Invoke(sender, eventArgs);
        }
    }
}
