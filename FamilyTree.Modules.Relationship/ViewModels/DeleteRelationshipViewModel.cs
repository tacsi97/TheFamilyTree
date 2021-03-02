using FamilyTree.Core;
using FamilyTree.Core.Commands;
using FamilyTree.Modules.Relationship.Core;
using FamilyTree.Services.Repository.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.Modules.Relationship.ViewModels
{
    public class DeleteRelationshipViewModel : BindableBase, INavigationAware
    {
        #region Fields

        private readonly IAsyncRepository<Business.Relationship> _repository;
        private readonly IRegionManager _regionManager;

        #endregion

        #region Properties

        private Business.Relationship _selectedRelationship;
        public Business.Relationship SelectedRelationship
        {
            get { return _selectedRelationship; }
            set { SetProperty(ref _selectedRelationship, value); }
        }

        #endregion

        #region Commands

        private AsyncCommand _saveCommand;
        public AsyncCommand SaveCommand =>
            _saveCommand ?? (_saveCommand = new AsyncCommand(ExecuteSaveCommand, CanExecuteSaveCommand));

        #endregion

        public DeleteRelationshipViewModel(IAsyncRepository<Business.Relationship> repository, IRegionManager regionManager)
        {
            _repository = repository;
            _regionManager = regionManager;
        }

        public async Task ExecuteSaveCommand()
        {
            try
            {
                await _repository.DeleteAsync(SelectedRelationship.ID);

                _regionManager.RequestNavigate(RegionNames.ContentRegion, "ListRelationshipView");
            }
            catch (Exception)
            {
                // TODO: do something
            }
        }

        public bool CanExecuteSaveCommand()
        {
            return true;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            SelectedRelationship = navigationContext.Parameters.GetValue<Business.Relationship>(NavParamNames.Relationship);
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
