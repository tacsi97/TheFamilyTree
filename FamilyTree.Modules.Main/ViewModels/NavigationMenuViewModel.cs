using FamilyTree.Business;
using FamilyTree.Core;
using FamilyTree.Core.ApplicationCommands;
using FamilyTree.Core.Mvvm;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
            MenuItems.Add(new NavigationItem() { Caption = "Főoldal", NavigationPath = "MainPage" });
            MenuItems.Add(new NavigationItem() { Caption = "Családfák", NavigationPath = "ListFamilyTreeView" });
            MenuItems.Add(new NavigationItem() { Caption = "Fa", NavigationPath = "ParentTreeView" });
        }

        void ExecuteSelectCommand(NavigationItem navigationItem)
        {
            // TODO: IsExpanded változóhoz legyen kötve a child objektum lista láthatósága
            _applicationCommand.CompositeCommand.Execute(navigationItem.NavigationPath);
        }
    }
}
