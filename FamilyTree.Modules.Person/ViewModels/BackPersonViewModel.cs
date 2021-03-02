using FamilyTree.Core;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FamilyTree.Modules.Person.ViewModels
{
    public class BackPersonViewModel : BindableBase
    {

        #region Fields

        private readonly IRegionManager _regionManager;

        #endregion

        #region Commands

        private DelegateCommand _navigateBackCommand;
        public DelegateCommand NavigateBackCommand =>
            _navigateBackCommand ?? (_navigateBackCommand = new DelegateCommand(ExecuteNavigateBackCommand, CanExecuteNavigateBackCommand));

        #endregion

        public BackPersonViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void ExecuteNavigateBackCommand()
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, "ListPersonView");
        }

        bool CanExecuteNavigateBackCommand()
        {
            return true;
        }
    }
}
