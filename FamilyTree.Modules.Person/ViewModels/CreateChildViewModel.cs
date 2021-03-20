using FamilyTree.Core;
using FamilyTree.Core.Commands;
using FamilyTree.Modules.Person.Core;
using FamilyTree.Services.Repository.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.Modules.Person.ViewModels
{
    public class CreateChildViewModel : BindableBase, INavigationAware
    {
        private readonly IAsyncRepository<Business.Person> _repository;
        private readonly IRegionManager _regionManager;

        #region Commands

        private AsyncCommand _asyncCommand;
        public AsyncCommand AsyncCommand
        {
            get { return _asyncCommand; }
            set { SetProperty(ref _asyncCommand, value); }
        }

        #endregion

        #region Properties

        private Business.Person _mother;
        public Business.Person Mother
        {
            get { return _mother; }
            set { SetProperty(ref _mother, value); }
        }

        private Business.Person _father;
        public Business.Person Father
        {
            get { return _father; }
            set { SetProperty(ref _father, value); }
        }

        private Business.Person _newPerson = new Business.Person();
        public Business.Person NewPerson
        {
            get { return _newPerson; }
            set { SetProperty(ref _newPerson, value); }
        }

        public string Title => "Személy létrehozása";

        #endregion

        public CreateChildViewModel(IAsyncRepository<Business.Person> repository, IRegionManager regionManager)
        {
            _repository = repository;
            _regionManager = regionManager;

            NewPerson = new Business.Person();

            AsyncCommand = new AsyncCommand(Submit, CanExecuteSubmit);
        }

        public async Task Submit()
        {
            try
            {
                await _repository.CreateAsync(NewPerson);

                _regionManager.RequestNavigate(RegionNames.ContentRegion, "ListPersonView");
            }
            catch (Exception e)
            {
                // TODO: exception handling
            }
        }

        public bool CanExecuteSubmit()
        {
            return true;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Father = navigationContext.Parameters.GetValue<Business.Person>(NavParamNames.Father);
            Mother = navigationContext.Parameters.GetValue<Business.Person>(NavParamNames.Mother);
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
