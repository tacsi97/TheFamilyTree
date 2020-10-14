using FamilyTree.Core;
using FamilyTree.Core.PubSubEvents;
using FamilyTree.Modules.Person.Commands;
using FamilyTree.Modules.Person.Core;
using FamilyTree.Services.Repository.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FamilyTree.Modules.Person.ViewModels
{
    public class PeopleListViewModel : BindableBase
    {
        private readonly IAsyncRepository<Business.Person> _repository;
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;

        #region Properties

        private Business.Person _selectedPerson;
        public Business.Person SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                SetProperty(ref _selectedPerson, value);
                _eventAggregator.GetEvent<SelectedPersonChangedEvent>().Publish(value);
            }
        }

        public ObservableCollection<Business.Person> People { get; set; }

        #endregion

        #region Commands

        public GetPeopleCommand GetPeopleCommand { get; set; }

        #endregion

        public PeopleListViewModel(IAsyncRepository<Business.Person> repository, IDialogService dialogService, IEventAggregator eventAggregator)
        {
            _repository = repository;
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;

            // TODO: Maybe change this to CommandFactory call
            GetPeopleCommand = new GetPeopleCommand(this);

            People = new ObservableCollection<Business.Person>();
        }

        public async Task ExecuteGetPeopleCommand()
        {
            People.Clear();

            var people = await _repository.GetAllAsync(Uris.PersonURI);

            foreach (var person in people)
            {
                People.Add(person);
            }
        }
    }
}
