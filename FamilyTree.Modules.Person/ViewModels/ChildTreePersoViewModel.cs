﻿using FamilyTree.Business;
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
    public class ChildTreePersoViewModel : BindableBase
    {
        #region Fields

        private readonly IAsyncRepository<Business.Person> _repository;
        private readonly ITreeTraversal<Node, Line> _treeTravelsal;

        #endregion

        #region Properties

        public Business.Person SelectedPerson { get; set; }

        public ObservableCollection<ITreeElement> TreeElements { get; set; }

        #endregion

        #region Commands

        private DelegateCommand _drawCommand;
        public DelegateCommand DrawCommand =>
            _drawCommand ?? (_drawCommand = new DelegateCommand(ExecuteDrawCommand));

        #endregion

        public ChildTreePersoViewModel(IAsyncRepository<Business.Person> repository, ITreeTraversal<Business.Node, Business.Line> treeTravelsal)
        {
            _repository = repository;
            _treeTravelsal = treeTravelsal;

            TreeElements = new ObservableCollection<ITreeElement>();
        }

        public void ExecuteDrawCommand()
        {
            TreeElements.Clear();
            _treeTravelsal.Nodes.Clear();
            _treeTravelsal.Lines.Clear();
            _treeTravelsal.PostOrder(new Node(SelectedPerson));
            FillTreeElements(_treeTravelsal.Nodes);
            FillTreeElements(_treeTravelsal.Lines);
            Offset();
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
            var lastNode = _treeTravelsal.Nodes.Last();
            var top = -lastNode.TopCoordinate;

            foreach (var node in TreeElements)
            {
                node.TopCoordinate += top;
                node.BottomCoordinate += top;
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