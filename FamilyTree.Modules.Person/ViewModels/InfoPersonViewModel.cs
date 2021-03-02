using FamilyTree.Core.Mvvm;
using FamilyTree.Modules.Person.Core;
using FamilyTree.Services.Repository.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FamilyTree.Modules.Person.ViewModels
{
    public class InfoPersonViewModel : BindableBase, INavigationAware
    {
        private Business.Person _person;
        public Business.Person Person
        {
            get { return _person; }
            set { SetProperty(ref _person, value); }
        }

        public InfoPersonViewModel()
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Person = navigationContext.Parameters.GetValue<Business.Person>(NavParamNames.Person);
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
