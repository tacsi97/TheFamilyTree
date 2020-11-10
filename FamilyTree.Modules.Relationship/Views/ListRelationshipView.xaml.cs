using FamilyTree.Core;
using FamilyTree.Core.Attributes;
using System.Windows.Controls;

namespace FamilyTree.Modules.Relationship.Views
{
    /// <summary>
    /// Interaction logic for EditRelationshipView
    /// </summary>
    [DependentView(RegionNames.FunctionRegion, typeof(RelationshipFunctions))]
    public partial class ListRelationshipView : UserControl
    {
        public ListRelationshipView()
        {
            InitializeComponent();
        }
    }
}
