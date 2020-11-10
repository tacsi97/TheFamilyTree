using FamilyTree.Business;
using FamilyTree.Core;
using FamilyTree.Core.Commands;
using FamilyTree.Modules.TreeDrawer.Model;
using FamilyTree.Modules.TreeDrawer.Utils;
using FamilyTree.Services.Repository.Interfaces;
using Prism.Commands;
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

        #endregion

        #region Properties

        public ObservableCollection<ITreeElement> TreeElements { get; set; }

        public Utils.TreeDrawer TreeDrawer { get; set; }

        public Utils.PersonConnector PersonConnector { get; set; }

        #endregion

        #region Commands

        private AsyncCommand _getRelationships;
        public AsyncCommand GetRelationships =>
            _getRelationships ?? (_getRelationships = new AsyncCommand(ExecuteGetRelationships));

        #endregion

        public ParentTreeViewModel(IAsyncRepository<Business.Relationship> repository)
        {
            _repository = repository;

            PersonConnector = new Utils.PersonConnector();
            TreeElements = new ObservableCollection<ITreeElement>();
        }

        public async Task ExecuteGetRelationships()
        {
            var testAlgo = new TestTree();

            TreeElements.Clear();
            // Az összes kapcsolat, ami a fához tartozik
            var result = await _repository.GetAllAsync(Uris.RelationshipsURI);

            PersonConnector.ConnectPeople(result);

            var aggregatedPeople = AggregatePeople(result);

            var peopleList = DictionaryToList(aggregatedPeople);

            var nodes = new List<Node>();

            peopleList.ToList().ForEach((person) =>
            {
                nodes.Add(
                    new Node(person, 100d, 100d)
                    );
            });

            TreeDrawer = new Utils.TreeDrawer(nodes);

            TreeDrawer.ArrangeUpperTree(nodes[0]).ToList().ForEach((node) =>
                TreeElements.Add(new NodeTreeElement(node)));

            TreeDrawer.Createlines().ToList().ForEach((line) =>
                TreeElements.Add(new LineTreeElement(line)));
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
