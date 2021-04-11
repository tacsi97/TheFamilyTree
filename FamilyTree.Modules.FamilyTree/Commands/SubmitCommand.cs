using FamilyTree.Core.Commands;
using FamilyTree.Core.Extensions;
using FamilyTree.Modules.FamilyTree.ViewModels;

namespace FamilyTree.Modules.FamilyTree.Commands
{
    public class SubmitCommand : CommandBase<CreateFamilyTreeViewModel>
    {
        public SubmitCommand(CreateFamilyTreeViewModel ViewModel) : base(ViewModel)
        {

        }

        public override bool CanExecute(object parameter) => ViewModel.SubmitCanExecute();

        public override void Execute(object parameter) => ViewModel.SubmitExecute().FireAndForgetAsync();
    }
}
