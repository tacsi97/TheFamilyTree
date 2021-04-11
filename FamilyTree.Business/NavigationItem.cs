using System.Collections.ObjectModel;

namespace FamilyTree.Business
{
    public class NavigationItem
    {
        public string Icon { get; set; }
        public string Caption { get; set; }
        public string NavigationPath { get; set; }

        public ObservableCollection<NavigationItem> NavigationItems { get; set; }

        public NavigationItem()
        {
            NavigationItems = new ObservableCollection<NavigationItem>();
        }
    }
}
