using FamilyTree.Core;
using FamilyTree.Core.Attributes;
using FamilyTree.Modules.Person.Views;
using System.Windows.Controls;

namespace FamilyTree.Modules.TreeDrawer.Views
{
    /// <summary>
    /// Interaction logic for ParentTreeView
    /// </summary>
    [DependentView(RegionNames.FunctionRegion, typeof(PersonFunctions))]
    public partial class ParentTreeView : UserControl
    {
        public ParentTreeView()
        {
            InitializeComponent();
        }
    }
}
