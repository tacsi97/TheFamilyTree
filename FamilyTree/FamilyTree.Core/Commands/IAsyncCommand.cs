using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FamilyTree.Core.Commands
{
    public interface SubmitAsyncCommand : ICommand
    {
        Task ExecuteAsync();
        bool CanExecute();
    }
}
