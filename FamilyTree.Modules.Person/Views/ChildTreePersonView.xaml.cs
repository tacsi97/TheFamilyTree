using FamilyTree.Core;
using FamilyTree.Core.Attributes;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
