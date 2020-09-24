using FamilyTree.Core.Commands;
using FamilyTree.Core.Extensions;
using FamilyTree.Modules.Person.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTree.Modules.Person.Commands
{
    public class GetPeopleCommand : CommandBase<PeopleListViewModel>
    {
        public GetPeopleCommand(PeopleListViewModel ViewModel) : base(ViewModel)
        {
        }

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            ViewModel.ExecuteGetPeopleCommand().FireAndForgetAsync();
        }
    }
}
