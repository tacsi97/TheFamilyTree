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

namespace FamilyTree.Modules.Person.ViewModels
{
    public class DeletePersonViewModel : BindableBase, IDialogAware
    {
        #region Fields

        private readonly IAsyncGraphRepository<Business.Person> _repository;

        #endregion

        #region Properties

        public string Title => "Törlés";

        private Business.Person _selectedPerson;
        public Business.Person SelectedPerson
        {
            get { return _selectedPerson; }
            set { SetProperty(ref _selectedPerson, value); }
        }

        #endregion

        #region Commands

        public AsyncCommand DeleteCommand { get; set; }

        #endregion

        #region Events

        public event Action<IDialogResult> RequestClose;

        #endregion

        public DeletePersonViewModel(IAsyncGraphRepository<Business.Person> repository)
        {
            _repository = repository;

            DeleteCommand = new AsyncCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
        }

        public async Task ExecuteDeleteCommand()
        {
            try
            {
                await _repository.DeleteAsync(SelectedPerson.ID);

                RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
            }
            catch(Exception e)
            {
                // TODO: handle exception
            }
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            SelectedPerson = parameters.GetValue<Business.Person>("SelectedPerson");
        }

        public bool CanExecuteDeleteCommand() => true;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {

        }
    }
}
