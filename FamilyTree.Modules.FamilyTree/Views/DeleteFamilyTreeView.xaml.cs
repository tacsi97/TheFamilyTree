using FamilyTree.Core;
using FamilyTree.Core.Attributes;
using System.Windows;
using System.Windows.Controls;

namespace FamilyTree.Modules.FamilyTree.Views
{
    /// <summary>
    /// Interaction logic for DeleteFamilyTreeView.xaml
    /// </summary>

    [DependentView(RegionNames.FunctionRegion, typeof(BackFamilyTreeView))]
    public partial class DeleteFamilyTreeView : UserControl
    {
        public DeleteFamilyTreeView()
        {
            InitializeComponent();
        }
    }
}
