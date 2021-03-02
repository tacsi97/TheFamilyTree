using FamilyTree.Core.Commands;
using FamilyTree.Core.Extensions;
using FamilyTree.Modules.Person.ViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTree.Modules.Person.Commands
{
    public class SubmitNewPersonCommand : CommandBase<CreatePersonViewModel>
    {
        public SubmitNewPersonCommand(CreatePersonViewModel ViewModel) : base(ViewModel)
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
