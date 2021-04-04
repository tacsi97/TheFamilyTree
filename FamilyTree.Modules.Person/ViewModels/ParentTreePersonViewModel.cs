using FamilyTree.Business;
using FamilyTree.Core.PubSubEvents;
using FamilyTree.Modules.Person.Core;
using FamilyTree.Services.Repository.Interfaces;
using FamilyTree.Services.TreeTravelsal;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FamilyTree.Modules.Person.ViewModels
{
    public class ParentTreePersonViewModel : BindableBase, INavigationAware
    {
        #region Fields

        private readonly IAsyncRepository<Business.Person> _repository;
        private readonly ParentTraverseBase _treeTravelsal;
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

        private DelegateCommand _drawCommand;
        public DelegateCommand DrawCommand =>
            _drawCommand ?? (_drawCommand = new DelegateCommand(ExecuteDrawCommand));

        #endregion

        public ParentTreePersonViewModel(IAsyncRepository<Business.Person> repository, ParentTraverseBase treeTravelsal, IEventAggregator eventAggregator)
        {
            _repository = repository;
            _treeTravelsal = treeTravelsal;
            _eventAggregator = eventAggregator;

            TreeElements = new ObservableCollection<ITreeElement>();
        }

        public void ExecuteDrawCommand()
        {
            TreeElements.Clear();
            _treeTravelsal.Nodes.Clear();
            _treeTravelsal.Lines.Clear();
            _treeTravelsal.LeftmostValue = 0;
            var root = new Node(SelectedPerson);
            _treeTravelsal.PostOrder(root.Person);
            FillTreeElements(_treeTravelsal.Nodes);
            FillTreeElements(_treeTravelsal.Lines);
            Offset();
            RemoveNodes(_treeTravelsal.Nodes);
        }

        public void RemoveNodes(ICollection<Node> nodes)
        {
            foreach (var node in nodes)
            {
                node.Person.Node = null;
            }
        }

        public void FillTreeElements(ICollection<Node> nodes)
        {
            foreach (var node in nodes)
            {
                TreeElements.Add(node);
            }
        }

        public void FillTreeElements(ICollection<Line> lines)
        {
            foreach (var line in lines)
            {
                TreeElements.Add(line);
            }
        }

        public void Offset()
        {
            var leftmostValue = 0d;
            var topmostValue = 0d;

            foreach (var treeElement in TreeElements)
            {
                leftmostValue = Math.Min(leftmostValue, treeElement.LeftCoordinate);
                topmostValue = Math.Min(topmostValue, treeElement.TopCoordinate);
            }

            foreach (var treeElement in TreeElements)
            {
                treeElement.LeftCoordinate += -leftmostValue;
                treeElement.RigthCoordinate += -leftmostValue;
                treeElement.TopCoordinate += -topmostValue;
                treeElement.BottomCoordinate += -topmostValue;
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            SelectedPerson = navigationContext.Parameters.GetValue<Business.Person>(NavParamNames.Person);
            ExecuteDrawCommand();

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
            return;
        }
    }
}
