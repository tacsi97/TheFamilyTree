using FamilyTree.Business;
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
        private readonly IRegionManager _regionManager;

        public ObservableCollection<NavigationItem> MenuItems { get; set; }

        public NavigationMenuViewModel(IRegionManager regionManager):
            base(regionManager)
        {
            _regionManager = regionManager;
            MenuItems = new ObservableCollection<NavigationItem>();
            InitializeMenu();
        }

        private void InitializeMenu()
        {
            MenuItems.Add(new NavigationItem() { Caption = "Főoldal" });
            MenuItems.Add(new NavigationItem() { Caption = "Családfák" });
            MenuItems.Add(new NavigationItem() { Caption = "Beállítások" });
            MenuItems.Add(new NavigationItem() { Caption = "Kijelentkezés" });
        }
    }
}
