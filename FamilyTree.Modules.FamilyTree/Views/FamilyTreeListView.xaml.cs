using FamilyTree.Core;
using FamilyTree.Core.Attributes;
using System.Windows.Controls;

namespace FamilyTree.Modules.FamilyTree.Views
{
    /// <summary>
    /// Interaction logic for FamilyTreeListView
    /// </summary>
    [DependentView(RegionNames.FunctionRegion, typeof(FamilyTreeFunctionView))]
    public partial class FamilyTreeListView : UserControl
    {
        public FamilyTreeListView()
        {
            InitializeComponent();
        }
    }
}
