using FamilyTree.Core;
using FamilyTree.Core.Commands;
using FamilyTree.Core.Extensions;
using FamilyTree.Services.Repository.Interfaces;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.Modules.Person.ViewModels
{
    public class NewPersonDialogViewModel : BindableBase, IDialogAware
    {
        private readonly IAsyncRepository<Business.Person> _repository;

        private IAsyncCommand _submitAsyncCommand;
        public IAsyncCommand SubmitAsyncCommand
        {
            get { return _submitAsyncCommand; }
            set { SetProperty(ref _submitAsyncCommand, value); }
        }

        private Business.Person _person = new Business.Person();
        public Business.Person Person
        {
            get { return _person; }
            set { SetProperty(ref _person, value); }
        }

        public string Title => "Személy létrehozása";

        public event Action<IDialogResult> RequestClose;

        public NewPersonDialogViewModel(IAsyncRepository<Business.Person> repository)
        {
            _repository = repository;
            SubmitAsyncCommand = new AsyncCommand(Submit, CanExecuteSubmit);
        }

        public async Task Submit()
        {
            await _repository.CreateAsync(JsonConvert.SerializeObject(Person), Uris.PersonURI);

            var result = new DialogResult(ButtonResult.OK);

            RequestClose(result);
        }

        public bool CanExecuteSubmit()
        {
            //TODO: add gender check
            if (Person == null 
                || string.IsNullOrEmpty(Person.FirstName)
                || string.IsNullOrEmpty(Person.LastName)
                || Person.DateOfBirth == null) 
                return false;
            return true;
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
