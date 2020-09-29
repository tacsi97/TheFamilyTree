using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTree.Modules.Person
{
    public class ViewTabItem : BindableBase
    {
        private string _caption;
        public string Caption
        {
            get { return _caption; }
            set { SetProperty(ref _caption, value); }
        }

        private string _regionName;
        public string RegionName
        {
            get { return _regionName; }
            set { SetProperty(ref _regionName, value); }
        }
    }
}
