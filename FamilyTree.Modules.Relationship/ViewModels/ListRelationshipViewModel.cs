using FamilyTree.Business;
using FamilyTree.Core;
using FamilyTree.Core.Attributes;
using FamilyTree.Core.Commands;
using FamilyTree.Core.PubSubEvents;
using FamilyTree.Modules.Relationship.Core;
using FamilyTree.Modules.Relationship.Views;
using FamilyTree.Services.Repository.Interfaces;
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

namespace FamilyTree.Modules.Relationship.ViewModels
{
    public class ListRelationshipViewModel : BindableBase, INavigationAware
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IAsyncRepository<Business.Relationship> _repository;

        public string Title => "Edit";

        #region Properties

        private Person _selectedPerson;
        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set { SetProperty(ref _selectedPerson, value); }
        }

        private Business.Relationship _selectedRelation;
        public Business.Relationship SelectedRelation
        {
            get { return _selectedRelation; }
            set
            {
                SetProperty(ref _selectedRelation, value);
                _eventAggregator.GetEvent<SelectedRelationshipChangedEvent>().Publish(value);
            }
        }

        public ObservableCollection<Business.Relationship> Relationships { get; set; }

        #endregion

        #region Commands

        private IAsyncCommand _getRelationshipsCommand;
        public IAsyncCommand GetRelationshipsCommand =>
            _getRelationshipsCommand ?? (_getRelationshipsCommand = new AsyncCommand(ExecuteGetRelationshipsCommand, CanExecuteGetRelationshipsCommand));

        #endregion

        public ListRelationshipViewModel(IEventAggregator eventAggregator, IAsyncRepository<Business.Relationship> repository)
        {
            _eventAggregator = eventAggregator;
            _repository = repository;

            // TODO: delete
            Relationships = new ObservableCollection<Business.Relationship>();
        }

        public async Task ExecuteGetRelationshipsCommand()
        {
            // TODO: Ha nem nyomjuk meg a mentés gombot, akkor is megváltoztatja, a Bind miatt, de csak a lokális változatot
            Relationships.Clear();
            // TODO: lekérdezés, ami az összes kapcsolatot lekéri ember id alapján
            var relships = await _repository.GetAllAsync();

            relships.ToList().ForEach((relship) => Relationships.Add(relship));
        }

        public bool CanExecuteGetRelationshipsCommand()
        {
            return true;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            SelectedPerson = navigationContext.Parameters.GetValue<Person>("SelectedPerson");
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
