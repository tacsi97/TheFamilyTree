using FamilyTree.Business;
using FamilyTree.Core;
using FamilyTree.Core.Commands;
using FamilyTree.Core.Extensions;
using FamilyTree.Core.PubSubEvents;
using FamilyTree.Services.PersonConnector.Interfaces;
using FamilyTree.Services.Repository.Interfaces;
using FamilyTree.Services.TreeDrawer.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace FamilyTree.Modules.TreeDrawer.ViewModels
{
    public class ParentTreeViewModel : BindableBase, INavigationAware
    {
        #region Fields

        private readonly IAsyncRepository<Business.Relationship> _repository;
        private readonly ITreeDrawer _treeDrawer;
        private readonly IPersonConnector _personConnector;
        private readonly IEventAggregator _eventAggregator;

        #endregion

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

        private Business.FamilyTree _selectedTree;
        public Business.FamilyTree SelectedTree
        {
            get { return _selectedTree; }
            set { SetProperty(ref _selectedTree, value); }
        }

        public ObservableCollection<ITreeElement> TreeElements { get; set; }

        #endregion

        #region Commands

        private AsyncCommand _getRelationships;
        public AsyncCommand GetRelationships =>
            _getRelationships ?? (_getRelationships = new AsyncCommand(ExecuteGetRelationships));

        private DelegateCommand<Business.Person> _selectPersonCommand;
        public DelegateCommand<Business.Person> SelectPersonCommand =>
            _selectPersonCommand ?? (_selectPersonCommand = new DelegateCommand<Business.Person>(ExecuteSelectPersonCommand));

        #endregion

        public ParentTreeViewModel(IAsyncRepository<Business.Relationship> repository, ITreeDrawer treeDrawer, IPersonConnector personConnector, IEventAggregator eventAggregator)
        {
            _repository = repository;
            _treeDrawer = treeDrawer;
            _personConnector = personConnector;
            _eventAggregator = eventAggregator;

            TreeElements = new ObservableCollection<ITreeElement>();
        }

        public async Task ExecuteGetRelationships()
        {
            //TreeElements.Clear();
            //// Az összes kapcsolat, ami a fához tartozik
            //var result = await _repository.GetRelationshipsIncludedIn(SelectedTree.ID);

            //var aggregatedPeople = AggregatePeople(result);

            //var peopleList = DictionaryToList(aggregatedPeople);

            //var nodes = new List<Node>();

            //peopleList.ToList().ForEach((person) =>
            //{
            //    nodes.Add(
            //        new Node(person, 100d, 100d)
            //        );
            //});

            //// TODO: Person repository is a kapcsolatokat kérdezze le, ott hajtsa végre az összekapcsolást, és adjon vissza egy emberek listát
            //_treeDrawer.SetNodes(nodes);

            //_treeDrawer.ArrangeUpperTree(nodes[0]).ToList().ForEach((node) =>
            //    TreeElements.Add(new NodeTreeElement(node)));

            //_treeDrawer.ArrangeLowerTree(nodes[0]).ToList().ForEach(node =>
            //    TreeElements.Add(new NodeTreeElement(node)));

            //_treeDrawer.Createlines().ToList().ForEach((line) =>
            //    TreeElements.Add(new LineTreeElement(line)));

            //Offset();
        }

        public void ExecuteSelectPersonCommand(Business.Person person)
        {
            SelectedPerson = person;
        }

        public void Offset()
        {
            var minX = 0d;
            var minY = 0d;
            TreeElements.ToList().ForEach(node =>
            {
                if (node.TopCoordinate < minX)
                    minX = node.TopCoordinate;

                if (node.LeftCoordinate < minY)
                    minY = node.LeftCoordinate;
            });

            TreeElements.ToList().ForEach(node =>
            {
                node.TopCoordinate += -minX;
                node.LeftCoordinate += -minY;
            });
        }

        private IDictionary<int, Business.Person> AggregatePeople(IEnumerable<Business.Relationship> relationships)
        {
            var dictionary = new Dictionary<int, Business.Person>();

            foreach (var relation in relationships)
            {
                dictionary[relation.PersonFrom.ID] = relation.PersonFrom;
                dictionary[relation.PersonTo.ID] = relation.PersonTo;
            }

            foreach (var relation in relationships)
            {
                if (relation.RelationType.Equals("Parent"))
                {
                    // connect parents
                    if (relation.PersonTo.Gender.Equals(GenderType.Male))
                        dictionary[relation.PersonFrom.ID].Father = dictionary[relation.PersonTo.ID];
                    else
                        dictionary[relation.PersonFrom.ID].Mother = dictionary[relation.PersonTo.ID];

                    dictionary[relation.PersonTo.ID].Children.Add(dictionary[relation.PersonFrom.ID]);
                }
                else if (relation.RelationType.Equals("Child"))
                {
                    // connect child
                    dictionary[relation.PersonFrom.ID].Children.Add(dictionary[relation.PersonTo.ID]);

                    if (relation.PersonFrom.Gender.Equals(GenderType.Male))
                        dictionary[relation.PersonTo.ID].Father = dictionary[relation.PersonFrom.ID];
                    else
                        dictionary[relation.PersonTo.ID].Mother = dictionary[relation.PersonFrom.ID];
                }
                else if (relation.RelationType.Equals("Partner"))
                {
                    // nem baj, ha a szülők és a gyerekek nem Relationship-ként vannak tárolva,
                    // mivel feltöltéskor Relationshipként megy fel,
                    // letöltéskor pedig szintén Relationshipként 
                    // módosításkor pedig lekérjük a Relationship objektumot
                    // connect partner
                    //dictionary[relation.PersonFrom.ID].Partners.Add(new Business.Relationship()
                    //{
                    //    RelationType = relation.RelationType,
                    //    From = relation.From,
                    //    To = relation.To,
                    //    PersonFrom = dictionary[relation.PersonFrom.ID],
                    //    PersonTo = dictionary[relation.PersonTo.ID]
                    //});

                    //dictionary[relation.PersonTo.ID].Partners.Add(new Business.Relationship()
                    //{
                    //    RelationType = relation.RelationType,
                    //    From = relation.From,
                    //    To = relation.To,
                    //    PersonFrom = dictionary[relation.PersonTo.ID],
                    //    PersonTo = dictionary[relation.PersonFrom.ID]
                    //});
                }

            }

            return dictionary;
        }

        private IEnumerable<Business.Person> DictionaryToList(IDictionary<int, Business.Person> dictionary)
        {
            var list = new List<Business.Person>();

            foreach (var keyValuePair in dictionary)
            {
                list.Add(keyValuePair.Value);
            }

            return list;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            SelectedTree = navigationContext.Parameters.GetValue<Business.FamilyTree>("SelectedTree");
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
