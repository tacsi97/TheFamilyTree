using FamilyTree.Core;
using FamilyTree.Core.Attributes;
using System.Windows.Controls;

namespace FamilyTree.Modules.Person.Views
{
    /// <summary>
    /// Interaction logic for DeletePersonView
    /// </summary>

    [DependentView(RegionNames.FunctionRegion, typeof(BackPersonView))]
    public partial class DeletePersonView : UserControl
    {
        public DeletePersonView()
        {
            InitializeComponent();
        }
    }
}
