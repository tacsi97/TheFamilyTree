using FamilyTree.Core;
using FamilyTree.Core.Commands;
using FamilyTree.Modules.FamilyTree.Core;
using FamilyTree.Services.Repository.Interfaces;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.Modules.FamilyTree.ViewModels
{
    public class ModifyTreeDialogViewModel : BindableBase, IDialogAware
    {
        // TODO: instead of passing the whole object, I should pass only the ID and the Name
        private readonly IAsyncGraphRepository<Business.FamilyTree> _repository;

        public string Title => "Fa módosítása";

        private Business.FamilyTree _familyTree;
        public Business.FamilyTree FamilyTree
        {
            get { return _familyTree; }
            set
            {
                SetProperty(ref _familyTree, value);
                ModifyCommand.RaiseCanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public IAsyncCommand ModifyCommand { get; set; }

        public event Action<IDialogResult> RequestClose;

        public ModifyTreeDialogViewModel(IAsyncGraphRepository<Business.FamilyTree> repository)
        {
            _repository = repository;

            ModifyCommand = new AsyncCommand(ExecuteModifyTreeCommand, CanExecuteModifyTreeCommand);
        }

        public async Task ExecuteModifyTreeCommand()
        {
            await _repository.ModifyAsync(
                    new Business.FamilyTree()
                    {
                        Name = FamilyTree.Name,
                        People = FamilyTree.People,
                        ID = FamilyTree.ID
                    });

            RequestClose(new DialogResult(ButtonResult.OK));
        }

        /// <summary>
        /// Determines that, the command can be executed or not.
        /// </summary>
        /// <returns>If the <c>NewTreeDialogViewModel</c>'s FamilyTreeName property is null or empty, then it return false.</returns>
        public bool CanExecuteModifyTreeCommand() => FamilyTree != null;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            FamilyTree = parameters.GetValue<Business.FamilyTree>(DialogParameterNames.Tree);
        }
    }
}

