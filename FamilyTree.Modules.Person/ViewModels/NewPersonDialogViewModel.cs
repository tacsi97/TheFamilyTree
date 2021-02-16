using FamilyTree.Business;
using FamilyTree.Core;
using FamilyTree.Core.ApplicationCommands;
using FamilyTree.Core.Commands;
using FamilyTree.Core.Extensions;
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
using System.Windows.Input;

namespace FamilyTree.Modules.Person.ViewModels
{
    public class NewPersonDialogViewModel : BindableBase, IDialogAware
    {
        private readonly IAsyncRemoteRepository<Business.Person> _repository;
        private readonly IEventAggregator _eventAggregator;

        #region Commands

        private SubmitNewPersonCommand _submitNewPersonCommand;
        public SubmitNewPersonCommand SubmitNewPersonCommand
        {
            get { return _submitNewPersonCommand; }
            set { SetProperty(ref _submitNewPersonCommand, value); }
        }

        private AsyncCommand _asyncCommand;
        public AsyncCommand AsyncCommand
        {
            get { return _asyncCommand; }
            set { SetProperty(ref _asyncCommand, value); }
        }

        private IUpload _applicationCommand;
        public IUpload ApplicationCommand
        {
            get { return _applicationCommand; }
            set { SetProperty(ref _applicationCommand, value); }
        }

        #endregion

        #region Properties
        private Business.Person _selectedPerson;
        public Business.Person SelectedPerson
        {
            get { return _selectedPerson; }
            set { SetProperty(ref _selectedPerson, value); }
        }

        private Business.Person _newPerson = new Business.Person();
        public Business.Person NewPerson
        {
            get { return _newPerson; }
            set { SetProperty(ref _newPerson, value); }
        }

        private int _id;
        public int ID
        {
            get { return _id; }
            set { SetProperty(ref _id, value);
            }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                SetProperty(ref _firstName, value);
                AsyncCommand.RaiseCanExecuteChanged(this, EventArgs.Empty);
                NewPerson.FirstName = value;
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                SetProperty(ref _lastName, value);
                AsyncCommand.RaiseCanExecuteChanged(this, EventArgs.Empty);
                NewPerson.LastName = value;
                // Ennek itt kell lennie, mivel a NewPersonDialogViewModel konstruktora, hamarabb lefut, mint a SetRelationShipViewModel-é
                _eventAggregator.GetEvent<CreateNewPersonEvent>().Publish(NewPerson);
                _eventAggregator.GetEvent<SelectedPersonChangedEvent>().Publish(SelectedPerson);
            }
        }

        private DateTime _dateOfBirth;
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                SetProperty(ref _dateOfBirth, value);
                AsyncCommand.RaiseCanExecuteChanged(this, EventArgs.Empty);
                NewPerson.DateOfBirth = value;
            }
        }

        private DateTime _dateOfDeath;
        public DateTime DateOfDeath
        {
            get { return _dateOfDeath; }
            set
            {
                SetProperty(ref _dateOfDeath, value);
                AsyncCommand.RaiseCanExecuteChanged(this, EventArgs.Empty);
                NewPerson.DateOfDeath = value;
            }
        }

        private GenderType _gender;
        public GenderType Gender
        {
            get { return _gender; }
            set
            {
                SetProperty(ref _gender, value);
                AsyncCommand.RaiseCanExecuteChanged(this, EventArgs.Empty);
                NewPerson.Gender = value;
            }
        }

        private Relationship _relationship;
        public Relationship Relationship
        {
            get { return _relationship; }
            set { SetProperty(ref _relationship, value); }
        }

        public string Title => "Személy létrehozása";

        #endregion

        public event Action<IDialogResult> RequestClose;

        public NewPersonDialogViewModel(IAsyncRemoteRepository<Business.Person> repository, IEventAggregator eventAggregator, IUpload uploadCommand)
        {
            _repository = repository;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<SelectedRelationshipChangedEvent>().Subscribe(SetRelationship);

            ApplicationCommand = uploadCommand;

            NewPerson = new Business.Person();

            AsyncCommand = new AsyncCommand(Submit, CanExecuteSubmit);

            SubmitNewPersonCommand = new SubmitNewPersonCommand(this);

            // TODO: Refactor...
            // Nem kell az IUpload, mert ApplicationCommand.<Command_name> alatt lehet elérni a paracsokat
            // CompositeCommand név helyett egy másikat kitalálni
            // Ehhez nem kell CanExecute, sem Execute, mivel a regisztrált Command-okon iterál végig
            // Modulokban is kijavítani a típus regisztrációt, singletont stb
            ApplicationCommand.CompositeCommand.RegisterCommand(AsyncCommand);
        }

        public async Task Submit()
        {
            try
            {
                NewPerson.ID = GlobalID.NewID();

                await _repository.CreateAsync(NewPerson);

                ApplicationCommand.CompositeCommand.UnregisterCommand(AsyncCommand);

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
                || string.IsNullOrEmpty(LastName))
                return false;
            return true;
        }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (!parameters.TryGetValue("SelectedPerson", out Business.Person person))
                throw new ArgumentException("Argument not found in the paramters", nameof(person));

            SelectedPerson = person;
            NewPerson.FamilyTree = SelectedPerson.FamilyTree;
        }

        public void SetRelationship(Business.Relationship relationship)
        {
            Relationship = relationship;
        }
    }

}
