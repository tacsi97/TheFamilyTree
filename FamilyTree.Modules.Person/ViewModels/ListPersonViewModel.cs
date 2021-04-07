using FamilyTree.Core.Extensions;
using FamilyTree.Core.PubSubEvents;
using FamilyTree.Modules.Person.Commands;
using FamilyTree.Modules.Person.Core;
using FamilyTree.Modules.Person.Extensions;
using FamilyTree.Services.Repository.Interfaces;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.Modules.Person.ViewModels
{
    public class ListPersonViewModel : BindableBase, INavigationAware
    {
        #region Fields

        private readonly IAsyncRepository<Business.Person> _repository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        #endregion

        #region Properties

        private Business.FamilyTree _selectedTree;
        public Business.FamilyTree SelectedTree
        {
            get { return _selectedTree; }
            set
            {
                SetProperty(ref _selectedTree, value);
                _eventAggregator.GetEvent<SelectedTreeChangedEvent>().Publish(value);
            }
        }

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

        public ListPersonViewModel(IAsyncRepository<Business.Person> repository, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _repository = repository;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;

            // TODO: Maybe change this to CommandFactory call
            GetPeopleCommand = new GetPeopleCommand(this);

            People = new ObservableCollection<Business.Person>();
        }

        public async Task ExecuteGetPeopleCommand()
        {
            People.Clear();

            var people = await _repository.GetPeopleAsync(SelectedTree);

            foreach (var person in people)
            {
                if (!string.IsNullOrEmpty(person.FatherID))
                {
                    person.Father = people.FirstOrDefault(p => p.ID == person.FatherID);
                    if (person.Father != null)
                        person.Father.Children.Add(person);
                }
                if (!string.IsNullOrEmpty(person.MotherID))
                {
                    person.Mother = people.FirstOrDefault(p => p.ID == person.MotherID);
                    if (person.Mother != null)
                        person.Mother.Children.Add(person);
                }
                if (!string.IsNullOrEmpty(person.PartnerID))
                {
                    person.Partner = people.FirstOrDefault(p => p.ID == person.PartnerID);
                    if (person.Partner != null)
                        person.Partner.Partner = person;
                }
                person.FamilyTree = SelectedTree;
                People.Add(person);
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.GetValue<Business.FamilyTree>(NavParamNames.Tree) is Business.FamilyTree tree)
                SelectedTree = tree;

            ExecuteGetPeopleCommand().FireAndForgetAsync();

            _eventAggregator.GetEvent<SelectedPersonChangedEvent>().Publish(
                SelectedPerson);
            _eventAggregator.GetEvent<SelectedViewTypeChangedEvent>().Publish(
                navigationContext.Parameters.GetValue<Business.ViewType>(NavParamNames.ViewType));
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
