using FamilyTree.Core;
using FamilyTree.Core.Attributes;
using System.Windows.Controls;

namespace FamilyTree.Modules.Person.Views
{
    /// <summary>
    /// Interaction logic for ParentTreePersonView
    /// </summary>

    [DependentView(RegionNames.FunctionRegion, typeof(PersonFunctions))]
    public partial class ParentTreePersonView : UserControl
    {
        public ParentTreePersonView()
        {
            InitializeComponent();
        }
    }
}
