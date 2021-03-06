using FamilyTree.Modules.Person.Core;
using FamilyTree.Services.Repository.Interfaces;
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

        #endregion

        #region Properties

        public Business.Person SelectedPerson { get; set; }

        #endregion

        #region Commands

        private DelegateCommand _drawCommand;
        public DelegateCommand DrawCommand =>
            _drawCommand ?? (_drawCommand = new DelegateCommand(ExecuteDrawCommand));

        #endregion

        public ParentTreePersonViewModel(IAsyncRepository<Business.Person> repository)
        {
            _repository = repository;
        }

        public void ExecuteDrawCommand()
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
