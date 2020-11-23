using FamilyTree.Core;
using FamilyTree.Core.Commands;
using FamilyTree.Services.Repository.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.Modules.Relationship.ViewModels
{
    public class DeleteRelationshipViewModel : BindableBase, IDialogAware
    {
        #region Fields

        private readonly IAsyncGraphRepository<Business.Relationship> _repository;

        #endregion

        #region Properties

        public string Title => "Törlés";

        private Business.Relationship _selectedRelationship;
        public Business.Relationship SelectedRelationship
        {
            get { return _selectedRelationship; }
            set { SetProperty(ref _selectedRelationship, value); }
        }

        #endregion

        #region Commands

        private AsyncCommand _saveCommand;
        public AsyncCommand SaveCommand =>
            _saveCommand ?? (_saveCommand = new AsyncCommand(ExecuteSaveCommand, CanExecuteSaveCommand));

        #endregion

        #region Events

        public event Action<IDialogResult> RequestClose;

        #endregion

        public DeleteRelationshipViewModel(IAsyncGraphRepository<Business.Relationship> repository)
        {
            _repository = repository;
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            SelectedRelationship = parameters.GetValue<Business.Relationship>("SelectedRelationship");
        }

        public async Task ExecuteSaveCommand()
        {
            try
            {
                await _repository.DeleteAsync(SelectedRelationship.ID);

                RequestClose(new DialogResult(ButtonResult.OK));
            }
            catch (Exception)
            {
                // TODO: do something
            }
        }

        public bool CanExecuteSaveCommand()
        {
            return true;
        }

    }
}
