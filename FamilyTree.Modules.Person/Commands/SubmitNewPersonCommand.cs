using FamilyTree.Core.Commands;
using FamilyTree.Core.Extensions;
using FamilyTree.Modules.Person.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTree.Modules.Person.Commands
{
    public class SubmitNewPersonCommand : CommandBase<NewPersonDialogViewModel>
    {
        public SubmitNewPersonCommand(NewPersonDialogViewModel ViewModel) : base(ViewModel)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return ViewModel.CanExecuteSubmit();
        }

        public override void Execute(object parameter)
        {
            ViewModel.Submit().FireAndForgetAsync();
        }
    }
}
