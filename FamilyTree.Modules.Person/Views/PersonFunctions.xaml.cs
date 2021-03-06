using FamilyTree.Core;
using FamilyTree.Core.Attributes;
using System.Windows.Controls;

namespace FamilyTree.Modules.Person.Views
{
    /// <summary>
    /// Interaction logic for PersonFunctions
    /// </summary>
    
    [DependentView(RegionNames.FunctionRegion, typeof(ParentTreePersonView))]
    public partial class PersonFunctions : UserControl
    {
        public PersonFunctions()
        {
            InitializeComponent();
        }
    }
}
