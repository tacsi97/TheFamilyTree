using FamilyTree.Core;
using FamilyTree.Core.ApplicationCommands;
using FamilyTree.Modules.FamilyTree.Repository;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Net.Http;
using System.Windows.Input;

namespace FamilyTree.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Fields

        private readonly IRegionManager _regionManager;
        private readonly IApplicationCommand _application;
        private readonly IDialogService _dialogService;

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

        public UselessCommand UselessCommand { get; set; }
        #endregion

        public MainWindowViewModel(IRegionManager regionManager, IApplicationCommand application, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _application = application;
            _dialogService = dialogService;
            _application.NavigateCommand.RegisterCommand(NavigationCommand);

            UselessCommand = new UselessCommand(this);
        }

        void ExecuteNavigationCommand(string navigationPath)
        {
            if (string.IsNullOrEmpty(navigationPath))
                throw new ArgumentNullException();

            _regionManager.RequestNavigate(RegionNames.ContentRegion, navigationPath);
        }

        public void ExecuteUseless()
        {
            _dialogService.ShowDialog(DialogNames.NewPersonDialog, null, r =>
            {
                
            });
        }
    }
}
