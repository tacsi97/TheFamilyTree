using FamilyTree.Core;
using FamilyTree.Core.Attributes;
using System.Windows.Controls;

namespace FamilyTree.Modules.FamilyTree.Views
{
    /// <summary>
    /// Interaction logic for ListFamilyTreeView
    /// </summary>
    [DependentView(RegionNames.FunctionRegion, typeof(FunctionFamilyTreeView))]
    public partial class ListFamilyTreeView : UserControl
    {
        public ListFamilyTreeView()
        {
            InitializeComponent();
        }
    }
}
