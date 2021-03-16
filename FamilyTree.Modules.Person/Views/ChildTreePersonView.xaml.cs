using FamilyTree.Core;
using FamilyTree.Core.Attributes;
using System.Windows.Controls;

namespace FamilyTree.Modules.Person.Views
{
    /// <summary>
    /// Interaction logic for ChildTreePersonView
    /// </summary>
    /// 
    [DependentView(RegionNames.FunctionRegion, typeof(PersonFunctions))]
    public partial class ChildTreePersonView : UserControl
    {
        public ChildTreePersonView()
        {
            InitializeComponent();
        }
    }
}
