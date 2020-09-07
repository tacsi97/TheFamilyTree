using FamilyTree.Business;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FamilyTree.Modules.Main.ViewModels
{
    public class NavigationMenuViewModel : BindableBase
    {
        public ObservableCollection<NavigationItem> MenuItems { get; set; }

        public NavigationMenuViewModel()
        {
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
