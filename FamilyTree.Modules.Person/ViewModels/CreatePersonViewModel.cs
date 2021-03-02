using FamilyTree.Business;
using FamilyTree.Core;
using FamilyTree.Core.ApplicationCommands;
using FamilyTree.Core.Commands;
using FamilyTree.Core.Extensions;
using FamilyTree.Core.PubSubEvents;
using FamilyTree.Modules.Person.Commands;
using FamilyTree.Modules.Person.Core;
using FamilyTree.Services.Repository.Interfaces;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FamilyTree.Modules.Person.ViewModels
{
    public class CreatePersonViewModel : BindableBase, INavigationAware
    {
        private readonly IAsyncRepository<Business.Person> _repository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

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
                // Ennek itt kell lennie, mivel a CreatePersonViewModel konstruktora, hamarabb lefut, mint a SetRelationShipViewModel-é
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

        public CreatePersonViewModel(IAsyncRepository<Business.Person> repository, IEventAggregator eventAggregator, IUpload uploadCommand, IRegionManager regionManager)
        {
            _repository = repository;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
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
                // TODO: Tranzakcióval lehet megoldani, nagyon egyszerűen
                await _repository.CreateAsync(NewPerson);
                ApplicationCommand.CompositeCommand.UnregisterCommand(AsyncCommand);

                _regionManager.RequestNavigate(RegionNames.ContentRegion, "ListPersonView");
            }
            catch (Exception e)
            {
                // TODO: exception handling
            }
        }

        /// <summary>
        /// This determines whether the command can be executed or not.
        /// If the <c>CreatePersonViewModel</c>'s FirstName or LastName attribute is null or empty, or the DateOfBirth is minvalue.
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

        public void SetRelationship(Business.Relationship relationship)
        {
            Relationship = relationship;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var person = navigationContext.Parameters.GetValue<Business.Person>(NavParamNames.Person);

            SelectedPerson = person;
            NewPerson.FamilyTree = person.FamilyTree;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            return;
        }
    }

}
