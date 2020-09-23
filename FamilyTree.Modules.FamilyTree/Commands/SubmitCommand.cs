using FamilyTree.Core.Commands;
using FamilyTree.Core.Extensions;
using FamilyTree.Modules.FamilyTree.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace FamilyTree.Modules.FamilyTree.Commands
{
    public class SubmitCommand : CommandBase<NewTreeDialogViewModel>
    {
        public SubmitCommand(NewTreeDialogViewModel ViewModel) : base(ViewModel)
        {

        }

        public override bool CanExecute(object parameter) => ViewModel.SubmitCanExecute();

        public override void Execute(object parameter) => ViewModel.SubmitExecute().FireAndForgetAsync();
    }
}
