using FamilyTree.Core;
using FamilyTree.Core.Attributes;
using System.Windows.Controls;

namespace FamilyTree.Modules.FamilyTree.Views
{
    /// <summary>
    /// Interaction logic for ModifyTreeDialog
    /// </summary>
    [DependentView(RegionNames.FunctionRegion, typeof(BackFamilyTreeView))]
    public partial class ModifyFamilyTreeView : UserControl
    {
        public ModifyFamilyTreeView()
        {
            InitializeComponent();
        }
    }
}
