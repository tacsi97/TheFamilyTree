using FamilyTree.Business;
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
using System.Windows.Input;

namespace FamilyTree.Modules.Person.ViewModels
{
    public class NewPersonDialogViewModel : BindableBase, IDialogAware
    {
        private readonly IAsyncRepository<Business.Person> _repository;

        private AsyncCommand _submitAsyncCommand;
        public AsyncCommand SubmitAsyncCommand
        {
            get { return _submitAsyncCommand; }
            set { SetProperty(ref _submitAsyncCommand, value); }
        }

        #region Properties

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                SetProperty(ref _firstName, value);
                SubmitAsyncCommand.RaiseCanExecuteChanged();
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                SetProperty(ref _lastName, value);
                SubmitAsyncCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTime _dateOfBirth;
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                SetProperty(ref _dateOfBirth, value);
                SubmitAsyncCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTime _dateOfDeath;
        public DateTime DateOfDeath
        {
            get { return _dateOfDeath; }
            set
            {
                SetProperty(ref _dateOfDeath, value);
                SubmitAsyncCommand.RaiseCanExecuteChanged();
            }
        }

        private GenderType _gender;
        public GenderType Gender
        {
            get { return _gender; }
            set
            {
                SetProperty(ref _gender, value);
                SubmitAsyncCommand.RaiseCanExecuteChanged();
            }
        }

        public string Title => "Személy létrehozása";

        #endregion

        public event Action<IDialogResult> RequestClose;

        public NewPersonDialogViewModel(IAsyncRepository<Business.Person> repository)
        {
            _repository = repository;
            SubmitAsyncCommand = new AsyncCommand(Submit, CanExecuteSubmit);
        }

        public async Task Submit()
        {
            await _repository.CreateAsync(JsonConvert.SerializeObject(
                new Business.Person()
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    DateOfBirth = DateOfBirth,
                    DateOfDeath = DateOfDeath,
                    Gender = Gender
                }), Uris.PersonURI);

            var result = new DialogResult(ButtonResult.OK);

            RequestClose(result);
        }

        public bool CanExecuteSubmit()
        {
            //TODO: add gender check
            if (string.IsNullOrEmpty(FirstName)
                || string.IsNullOrEmpty(LastName)
                || DateOfBirth == null)
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
