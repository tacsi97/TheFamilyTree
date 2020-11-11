using FamilyTree.Business;
using FamilyTree.Core;
using FamilyTree.Core.Commands;
using FamilyTree.Core.PubSubEvents;
using FamilyTree.Modules.TreeDrawer.Model;
using FamilyTree.Modules.TreeDrawer.Utils;
using FamilyTree.Services.PersonConnector.Interfaces;
using FamilyTree.Services.Repository.Interfaces;
using FamilyTree.Services.TreeDrawer.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace FamilyTree.Modules.TreeDrawer.ViewModels
{
    public class ParentTreeViewModel : BindableBase
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
            var testAlgo = new TestTree();

            TreeElements.Clear();
            // Az összes kapcsolat, ami a fához tartozik
            var result = await _repository.GetAllAsync(Uris.RelationshipsURI);

            _personConnector.ConnectPeople(result);

            var aggregatedPeople = AggregatePeople(result);

            var peopleList = DictionaryToList(aggregatedPeople);

            var nodes = new List<Node>();

            peopleList.ToList().ForEach((person) =>
            {
                nodes.Add(
                    new Node(person, 100d, 100d)
                    );
            });

            // TODO: Person repository is a kapcsolatokat kérdezze le, ott hajtsa végre az összekapcsolást, és adjon vissza egy emberek listát
            _treeDrawer.SetNodes(nodes);

            _treeDrawer.ArrangeUpperTree(nodes[0]).ToList().ForEach((node) =>
                TreeElements.Add(new NodeTreeElement(node)));

            _treeDrawer.Createlines().ToList().ForEach((line) =>
                TreeElements.Add(new LineTreeElement(line)));
        }

        public void ExecuteSelectPersonCommand(Business.Person person)
        {
            SelectedPerson = person;
        }

        private IDictionary<int, Business.Person> AggregatePeople(IEnumerable<Business.Relationship> relationships)
        {
            var dictionary = new Dictionary<int, Business.Person>();

            foreach (var relation in relationships)
            {
                dictionary[relation.PersonFrom.ID] = relation.PersonFrom;
                dictionary[relation.PersonTo.ID] = relation.PersonTo;
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

    }
}
