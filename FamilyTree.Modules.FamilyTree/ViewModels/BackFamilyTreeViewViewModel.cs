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
        #region Commands

        private DelegateCommand _navigateBackCommand;
        private readonly IRegionManager _regionManager;

        public DelegateCommand NavigateBackCommand =>
            _navigateBackCommand ?? (_navigateBackCommand = new DelegateCommand(ExecuteNavigateBackCommand, CanExecuteNavigateBackCommand));

        #endregion

        public BackFamilyTreeViewViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

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
