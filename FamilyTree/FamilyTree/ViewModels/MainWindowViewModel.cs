using FamilyTree.Core;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace FamilyTree.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Fields

        private readonly IRegionManager _regionManager;

        #endregion

        #region Full properties

        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        #endregion

        #region Commands

        private DelegateCommand<string> _navigationCommand;

        public DelegateCommand<string> NavigationCommand =>
            _navigationCommand ?? (_navigationCommand = new DelegateCommand<string>(ExecuteNavigationCommand));

        #endregion

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        void ExecuteNavigationCommand(string navigationPath)
        {
            if (string.IsNullOrEmpty(navigationPath))
                throw new ArgumentNullException();

            _regionManager.RequestNavigate(RegionNames.ContentRegion, navigationPath);
        }
    }
}
