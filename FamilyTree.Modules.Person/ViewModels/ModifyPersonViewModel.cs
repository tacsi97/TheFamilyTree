using FamilyTree.Business;
using FamilyTree.Core;
using FamilyTree.Core.Commands;
using FamilyTree.Core.PubSubEvents;
using FamilyTree.Modules.Person.Commands;
using FamilyTree.Services.Repository.Interfaces;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.Modules.Person.ViewModels
{
    public class ModifyPersonViewModel : BindableBase, IDialogAware
    {
        private readonly IAsyncRepository<Business.Person> _repository;

        private AsyncCommand _asyncCommand;
        public AsyncCommand AsyncCommand
        {
            get { return _asyncCommand; }
            set { SetProperty(ref _asyncCommand, value); }
        }

        #region Properties
        private int _id;
        public int ID
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                SetProperty(ref _firstName, value);
                AsyncCommand.RaiseCanExecuteChanged();
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                SetProperty(ref _lastName, value);
                AsyncCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTime _dateOfBirth;
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                SetProperty(ref _dateOfBirth, value);
                AsyncCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTime _dateOfDeath;
        public DateTime DateOfDeath
        {
            get { return _dateOfDeath; }
            set
            {
                SetProperty(ref _dateOfDeath, value);
                AsyncCommand.RaiseCanExecuteChanged();
            }
        }

        private GenderType _gender;
        public GenderType Gender
        {
            get { return _gender; }
            set
            {
                SetProperty(ref _gender, value);
                AsyncCommand.RaiseCanExecuteChanged();
            }
        }

        public string Title => "Személy létrehozása";

        public ObservableCollection<Business.Person> People { get; set; }

        #endregion

        public event Action<IDialogResult> RequestClose;

        public ModifyPersonViewModel(IAsyncRepository<Business.Person> repository)
        {
            _repository = repository;

            AsyncCommand = new AsyncCommand(ExecuteSubmit, CanExecuteSubmit);
        }

        public async Task ExecuteSubmit()
        {
            try
            {
                var person = new Business.Person()
                {
                    ID = ID,
                    FirstName = FirstName,
                    LastName = LastName,
                    DateOfBirth = DateOfBirth,
                    DateOfDeath = DateOfDeath,
                    Gender = Gender
                };

                await _repository.ModifyAsync(
                    Uris.PersonURI,
                    JsonConvert.SerializeObject(person));

                RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
            }
            catch (Exception e)
            {
                // TODO: exception handling
            }
        }

        /// <summary>
        /// This determines whether the command can be executed or not.
        /// If the <c>NewPersonDialogViewModel</c>'s FirstName or LastName attribute is null or empty, or the DateOfBirth is minvalue.
        /// </summary>
        /// <returns>True or false</returns>
        public bool CanExecuteSubmit()
        {
            // TODO: Create a generic validator class, what takes a parameter with the given type, and returns that the value is valid or not
            if (string.IsNullOrEmpty(FirstName)
                || string.IsNullOrEmpty(LastName)
                || DateOfBirth == DateTime.MinValue)
                return false;
            return true;
        }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            var person = parameters.GetValue<Business.Person>("SelectedPerson");

            ID = person.ID;
            FirstName = person.FirstName;
            LastName = person.LastName;
            DateOfBirth = person.DateOfBirth;
            DateOfDeath = person.DateOfDeath;
            Gender = person.Gender;
        }
    }
}
