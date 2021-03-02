using FamilyTree.Core;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FamilyTree.Modules.FamilyTree.ViewModels
{
    public class BackFamilyTreeViewViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        #region Commands

        private DelegateCommand _navigateBackCommand;

        public DelegateCommand NavigateBackCommand =>
            _navigateBackCommand ?? (_navigateBackCommand = new DelegateCommand(ExecuteNavigateBackCommand, CanExecuteNavigateBackCommand));

        #endregion

        public BackFamilyTreeViewViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        // TODO: Dependent View-t kirakni egy külön helyre, mert mindenhol ugyan az.
        public void ExecuteNavigateBackCommand()
        {
            var navParams = new NavigationParameters();

            _regionManager.RequestNavigate(RegionNames.ContentRegion, "ListFamilyTreeView", navParams);
        }

        public bool CanExecuteNavigateBackCommand()
        {
            return true;
        }
    }
}
