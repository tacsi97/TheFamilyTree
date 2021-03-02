using FamilyTree.Core;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FamilyTree.Modules.Person.ViewModels
{
    public class BackPersonViewViewModel : BindableBase
    {

        #region Fields

        private readonly IRegionManager _regionManager;

        #endregion

        #region Commands

        private DelegateCommand _navigateCommand;

        public DelegateCommand NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand(ExecuteNavigateCommand));

        #endregion

        public BackPersonViewViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void ExecuteNavigateCommand()
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, "ListPersonView");
        }
    }
}
