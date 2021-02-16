using FamilyTree.Core;
using FamilyTree.Core.ApplicationCommands;
using FamilyTree.Core.Commands;
using FamilyTree.Core.PubSubEvents;
using FamilyTree.Modules.Relationship.Core;
using FamilyTree.Services.Repository.Interfaces;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.Modules.Relationship.ViewModels
{
    public class SetRelationShipViewModel : BindableBase
    {
        #region Fields

        private readonly IEventAggregator _eventAggregator;
        private readonly IAsyncRemoteRepository<Business.Relationship> _repository;
        private readonly IUpload _appSaveCommand;

        #endregion

        #region Properties

        public ObservableCollection<string> Types { get; set; }

        private Business.Person _selectedPerson;
        public Business.Person SelectedPerson
        {
            get { return _selectedPerson; }
            set { SetProperty(ref _selectedPerson, value); }
        }

        private Business.Person _newPerson;
        public Business.Person NewPerson
        {
            get { return _newPerson; }
            set { SetProperty(ref _newPerson, value); }
        }

        private DateTime _from;
        public DateTime From
        {
            get { return _from; }
            set
            {
                SetProperty(ref _from, value);
                SaveCommand.RaiseCanExecuteChanged(this, EventArgs.Empty);
            }
        }

        private DateTime _to;
        public DateTime To
        {
            get { return _to; }
            set
            {
                SetProperty(ref _to, value);
                SaveCommand.RaiseCanExecuteChanged(this, EventArgs.Empty);
            }
        }

        private string _selectedType;
        public string SelectedType
        {
            get { return _selectedType; }
            set
            {
                SetProperty(ref _selectedType, value);
                SaveCommand.RaiseCanExecuteChanged(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Commands

        private IAsyncCommand _saveCommand;
        public IAsyncCommand SaveCommand =>
            _saveCommand ?? (_saveCommand = new AsyncCommand(ExecuteSaveCommand, CanExecuteSaveCommand));

        #endregion

        public SetRelationShipViewModel(IEventAggregator eventAggregator, IAsyncRemoteRepository<Business.Relationship> repository, IUpload saveCommand)
        {
            _eventAggregator = eventAggregator;
            _appSaveCommand = saveCommand;
            _repository = repository;

            _eventAggregator.GetEvent<CreateNewPersonEvent>().Subscribe(SetNewPerson);
            _eventAggregator.GetEvent<SelectedPersonChangedEvent>().Subscribe(SetSelectedPerson);

            _appSaveCommand.CompositeCommand.RegisterCommand(SaveCommand);

            Types = new ObservableCollection<string>();
            Types.Add(TypeNames.Parent);
            Types.Add(TypeNames.Partner);
            Types.Add(TypeNames.Child);
        }

        ~SetRelationShipViewModel()
        {
            _eventAggregator.GetEvent<CreateNewPersonEvent>().Unsubscribe(SetNewPerson);
        }

        public async Task ExecuteSaveCommand()
        {
            try
            {
                var relationship = new Business.Relationship()
                {
                    RelationType = SelectedType,
                    From = From,
                    To = To,
                    PersonFrom = SelectedPerson,
                    PersonTo = NewPerson
                };

                await _repository.CreateAsync(relationship);

                _appSaveCommand.CompositeCommand.UnregisterCommand(SaveCommand);
            }
            catch (Exception ex)
            {
                //TODO: do something
            }
        }

        public bool CanExecuteSaveCommand()
        {
            if (From == null
                || From == DateTime.MinValue)
                return false;

            if (To == null
                || To == DateTime.MinValue)
                return false;

            return true;
        }

        public void SetNewPerson(Business.Person person)
        {
            NewPerson = person;
        }

        private void SetSelectedPerson(Business.Person person)
        {
            SelectedPerson = person;
        }

    }
}
