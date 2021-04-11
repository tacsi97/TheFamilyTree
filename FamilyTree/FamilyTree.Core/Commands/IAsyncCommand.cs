using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FamilyTree.Core.Commands
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();
        bool CanExecute();
        void RaiseCanExecuteChanged(object sender, EventArgs eventArgs);
    }
}
