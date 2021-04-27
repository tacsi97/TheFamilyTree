using FamilyTree.Business;
using FamilyTree.Core.ApplicationCommands;
using FamilyTree.Core.Mvvm;
using Prism.Commands;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace FamilyTree.Modules.Main.ViewModels
{
    public class NavigationMenuViewModel : RegionViewModelBase
    {
        #region Fields

        private readonly IRegionManager _regionManager;
        private readonly IApplicationCommand _applicationCommand;

        #endregion

        #region Properties

        public ObservableCollection<NavigationItem> MenuItems { get; set; }

        #endregion

        #region Commands

        private DelegateCommand<NavigationItem> _selectCommand;
        public DelegateCommand<NavigationItem> SelectCommand =>
            _selectCommand ?? (_selectCommand = new DelegateCommand<NavigationItem>(ExecuteSelectCommand));

        #endregion

        public NavigationMenuViewModel(IRegionManager regionManager, IApplicationCommand applicationCommand) :
            base(regionManager)
        {
            _regionManager = regionManager;
            _applicationCommand = applicationCommand;

            MenuItems = new ObservableCollection<NavigationItem>();

            InitializeMenu();
        }

        private void InitializeMenu()
        {
            //The navigation path should match with the .xaml name
            MenuItems.Add(new NavigationItem() { Icon = "Home", Caption = " Főoldal", NavigationPath = "MainPage" });
            MenuItems.Add(new NavigationItem() { Icon = "Tree", Caption = " Családfák", NavigationPath = "ListFamilyTreeView" });
        }

        void ExecuteSelectCommand(NavigationItem navigationItem)
        {
            if (navigationItem == null) return;
            // TODO: IsExpanded változóhoz legyen kötve a child objektum lista láthatósága
            _applicationCommand.CompositeCommand.Execute(navigationItem.NavigationPath);
        }
    }
}
