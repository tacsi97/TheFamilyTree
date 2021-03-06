using FamilyTree.Business;
using FamilyTree.Modules.Person.Core;
using FamilyTree.Services.Repository.Interfaces;
using FamilyTree.Services.TreeTravelsal.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FamilyTree.Modules.Person.ViewModels
{
    public class ParentTreePersonViewModel : BindableBase, INavigationAware
    {
        #region Fields

        private readonly IAsyncRepository<Business.Person> _repository;
        private readonly ITreeTraversal<Node> _treeTravelsal;

        #endregion

        #region Properties

        public Business.Person SelectedPerson { get; set; }

        private string _outputString;
        public string OutputString
        {
            get { return _outputString; }
            set { SetProperty(ref _outputString, value); }
        }

        public ObservableCollection<Business.Node> Nodes { get; set; }

        #endregion

        #region Commands

        private DelegateCommand _drawCommand;
        public DelegateCommand DrawCommand =>
            _drawCommand ?? (_drawCommand = new DelegateCommand(ExecuteDrawCommand));

        #endregion

        public ParentTreePersonViewModel(IAsyncRepository<Business.Person> repository, ITreeTraversal<Business.Node> treeTravelsal)
        {
            _repository = repository;
            _treeTravelsal = treeTravelsal;

            Nodes = new ObservableCollection<Node>();
        }

        public void ExecuteDrawCommand()
        {
            _treeTravelsal.Nodes.Clear();
            _treeTravelsal.PostOrder(new Node(SelectedPerson));
            FillNodes(_treeTravelsal.Nodes);
            Offset();
        }

        public void FillNodes(ICollection<Node> nodes)
        {
            Nodes.Clear();

            foreach (var node in nodes)
            {
                Nodes.Add(node);
            }
        }

        public void Offset()
        {
            var firstNode = Nodes.ElementAt(0);
            var left = - firstNode.LeftCoordinate;
            var top = - firstNode.TopCoordinate;

            foreach (var node in Nodes)
            {
                node.TopCoordinate += top;
                node.LeftCoordinate += left;
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            SelectedPerson = navigationContext.Parameters.GetValue<Business.Person>(NavParamNames.Person);
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
