using FamilyTree.Core;
using FamilyTree.Core.Attributes;
using System.Windows.Controls;

namespace FamilyTree.Modules.Relationship.Views
{
    /// <summary>
    /// Interaction logic for DeleteRelationshipView
    /// </summary>
    
    [DependentView(RegionNames.ContentRegion, typeof(BackRelationshipView))]
    public partial class DeleteRelationshipView : UserControl
    {
        public DeleteRelationshipView()
        {
            InitializeComponent();
        }
    }
}
