using FamilyTree.Core.Extensions;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FamilyTree.Core.Commands
{
    public class AsyncCommand : IAsyncCommand
    {
        public event EventHandler CanExecuteChanged;

        private bool _isExecuting;
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;

        public AsyncCommand(
            Func<Task> execute,
            Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute()
        {
            return !_isExecuting && (_canExecute?.Invoke() ?? true);
        }

        public async Task ExecuteAsync()
        {
            if (CanExecute())
            {
                try
                {
                    _isExecuting = true;
                    await _execute();
                }
                finally
                {
                    _isExecuting = false;
                }
            }
        }

        public void RaiseCanExecuteChanged(object sender, EventArgs eventArgs)
        {
            CanExecuteChanged?.Invoke(sender, eventArgs);
        }

        #region Explicit implementations
        bool ICommand.CanExecute(object parameter)
        {
            _ = parameter;
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            _ = parameter;
            ExecuteAsync().FireAndForgetAsync();
        }
        #endregion
    }
}
