using FamilyTree.Core.Commands;
using FamilyTree.Core.Extensions;
using FamilyTree.Modules.FamilyTree.ViewModels;
using FamilyTree.Modules.FamilyTree.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTree.Modules.FamilyTree.Commands
{
    public class GetTreesCommand : CommandBase<ListFamilyTreeViewModel>
    {
        public GetTreesCommand(ListFamilyTreeViewModel ViewModel) : base(ViewModel)
        {
        }

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            ViewModel.ExecuteGetTreesCommand().FireAndForgetAsync();
        }
    }
}
