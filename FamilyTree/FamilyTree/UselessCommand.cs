using FamilyTree.ViewModels;
using System;
using System.Windows.Input;

namespace FamilyTree
{
    public class UselessCommand : ICommand
    {
        private readonly MainWindowViewModel _mainWindowViewModel;

        public event EventHandler CanExecuteChanged;

        public UselessCommand(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _mainWindowViewModel.ExecuteUseless();
        }
    }
}
