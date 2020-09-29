using FamilyTree.Core;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FamilyTree.Modules.Person.ViewModels
{
    public class PeopleTabViewModel : BindableBase
    {
        public ObservableCollection<ViewTabItem> Tabs { get; set; }

        public PeopleTabViewModel()
        {
            Tabs = new ObservableCollection<ViewTabItem>();
            InitTabs();
        }

        public void InitTabs()
        {
            Tabs.Add(new ViewTabItem() { Caption = "Lista nézet", RegionName = Core.RegionNames.ListView });
            Tabs.Add(new ViewTabItem() { Caption = "Felmenő nézet", RegionName = Core.RegionNames.ParentView });
            Tabs.Add(new ViewTabItem() { Caption = "Család nézet", RegionName = Core.RegionNames.FamilyView });
        }
    }
}
