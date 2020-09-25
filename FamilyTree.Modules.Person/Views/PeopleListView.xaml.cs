using FamilyTree.Core;
using FamilyTree.Core.Attributes;
using System.Windows.Controls;

namespace FamilyTree.Modules.Person.Views
{
    /// <summary>
    /// Interaction logic for PeopleList
    /// </summary>
    [DependentView(RegionNames.FunctionRegion, typeof(PersonFunctions))]
    public partial class PeopleListView : UserControl
    {
        public PeopleListView()
        {
            InitializeComponent();
        }
    }
}
