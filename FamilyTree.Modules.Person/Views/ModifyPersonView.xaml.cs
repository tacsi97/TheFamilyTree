using FamilyTree.Core;
using FamilyTree.Core.Attributes;
using System.Windows.Controls;

namespace FamilyTree.Modules.Person.Views
{
    /// <summary>
    /// Interaction logic for ModifyPersonView
    /// </summary>

    [DependentView(RegionNames.FunctionRegion, typeof(BackPersonView))]
    public partial class ModifyPersonView : UserControl
    {
        public ModifyPersonView()
        {
            InitializeComponent();
        }
    }
}
