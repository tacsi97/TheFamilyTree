using FamilyTree.Core;
using FamilyTree.Core.ApplicationCommands;
using FamilyTree.Modules.FamilyTree.Repository;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Net.Http;

namespace FamilyTree.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Fields

        private readonly IRegionManager _regionManager;
        private readonly IApplicationCommand _application;

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

        public MainWindowViewModel(IRegionManager regionManager, IApplicationCommand application)
        {
            _regionManager = regionManager;
            _application = application;
            _application.NavigateCommand.RegisterCommand(NavigationCommand);

        }

        void ExecuteNavigationCommand(string navigationPath)
        {
            if (string.IsNullOrEmpty(navigationPath))
                throw new ArgumentNullException();

            _regionManager.RequestNavigate(RegionNames.ContentRegion, navigationPath);
        }
    }
}
