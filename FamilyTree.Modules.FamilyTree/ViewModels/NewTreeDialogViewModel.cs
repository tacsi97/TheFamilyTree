using FamilyTree.Core;
using FamilyTree.Modules.FamilyTree.Commands;
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
    public class NewTreeDialogViewModel : BindableBase, IDialogAware
    {
        private readonly IAsyncGraphRepository<Business.FamilyTree> _repository;

        public string Title => "Fa létrehozása";

        private string _familyTreeName;
        public string FamilyTreeName
        {
            get { return _familyTreeName; }
            set
            {
                SetProperty(ref _familyTreeName, value);
                SubmitCommand.RaiseCanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public SubmitCommand SubmitCommand { get; set; }

        public event Action<IDialogResult> RequestClose;

        public NewTreeDialogViewModel(IAsyncGraphRepository<Business.FamilyTree> repository)
        {
            _repository = repository;

            SubmitCommand = new SubmitCommand(this);
        }

        /// <summary>
        /// Determines that, the command can be executed or not.
        /// </summary>
        /// <returns>If the <c>NewTreeDialogViewModel</c>'s FamilyTreeName property is null or empty, then it return false.</returns>
        public bool SubmitCanExecute() => !string.IsNullOrEmpty(FamilyTreeName);

        public async Task SubmitExecute()
        {
            await _repository.CreateAsync(
                    new Business.FamilyTree()
                    {
                        ID = GlobalID.NewID(),
                        Name = FamilyTreeName,
                        People = new ObservableCollection<Business.Person>()
                    });

            RequestClose(new DialogResult(ButtonResult.OK));
        }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }
    }
}
