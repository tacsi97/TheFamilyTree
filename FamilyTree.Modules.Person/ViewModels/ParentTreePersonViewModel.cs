using FamilyTree.Business;
using FamilyTree.Modules.Person.Core;
using FamilyTree.Services.Repository.Interfaces;
using FamilyTree.Services.TreeTravelsal.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FamilyTree.Modules.Person.ViewModels
{
    public class ParentTreePersonViewModel : BindableBase, INavigationAware
    {
        #region Fields

        private readonly IAsyncRepository<Business.Person> _repository;
        private readonly ITreeTravelsalText<Node> _treeTravelsal;

        #endregion

        #region Properties

        public Business.Person SelectedPerson { get; set; }

        private string _outputString;
        public string OutputString
        {
            get { return _outputString; }
            set { SetProperty(ref _outputString, value); }
        }

        #endregion

        #region Commands

        private DelegateCommand _drawCommand;
        public DelegateCommand DrawCommand =>
            _drawCommand ?? (_drawCommand = new DelegateCommand(ExecuteDrawCommand));

        #endregion

        public ParentTreePersonViewModel(IAsyncRepository<Business.Person> repository, ITreeTravelsalText<Business.Node> treeTravelsal)
        {
            _repository = repository;
            _treeTravelsal = treeTravelsal;
        }

        public void ExecuteDrawCommand()
        {
            _treeTravelsal.Builder.Clear();
            var root = new Node(SelectedPerson);
            _treeTravelsal.PostOrder(root);
            OutputString = _treeTravelsal.Builder.ToString();
        }

        public void GetPeopleInTreeForm()
        {

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
