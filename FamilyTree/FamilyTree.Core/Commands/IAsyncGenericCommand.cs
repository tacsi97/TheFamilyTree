using System.Threading.Tasks;
using System.Windows.Input;

namespace FamilyTree.Core.Commands
{
    public interface IAsyncGenericCommand<T> : ICommand
    {
        Task ExecuteAsync(T param);
        bool CanExecute(T param);
        void RaiseCanExecuteChanged();
    }
}
