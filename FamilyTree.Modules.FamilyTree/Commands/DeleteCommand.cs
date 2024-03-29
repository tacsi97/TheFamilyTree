﻿using FamilyTree.Core.Commands;
using FamilyTree.Core.Extensions;
using FamilyTree.Modules.FamilyTree.ViewModels;

namespace FamilyTree.Modules.FamilyTree.Commands
{
    public class DeleteCommand : CommandBase<DeleteFamilyTreeViewViewModel>
    {

        public DeleteCommand(DeleteFamilyTreeViewViewModel ViewModel) : base(ViewModel)
        {

        }

        public override bool CanExecute(object parameter)
        {
            return ViewModel.CanExecuteDeleteCommand();
        }

        public override void Execute(object parameter)
        {
            ViewModel.ExecuteDeleteCommand().FireAndForgetAsync();
        }
    }
}
