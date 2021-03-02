using FamilyTree.Core;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FamilyTree.Modules.Relationship.ViewModels
{
    public class BackRelationshipViewModel : BindableBase
    {
        #region Fields

        private readonly IRegionManager _regionManager;

        #endregion

        #region Commands

        private DelegateCommand _navigateBackCommand;
        public DelegateCommand NavigateBackCommand =>
            _navigateBackCommand ?? (_navigateBackCommand = new DelegateCommand(ExecuteNavigateBackCommand));

        #endregion

        public BackRelationshipViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void ExecuteNavigateBackCommand()
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, "ListRelationshipView");
        }

    }
}
