using FamilyTree.Core;
using FamilyTree.Core.Attributes;
using System.Windows.Controls;

namespace FamilyTree.Modules.Main.Views
{
    /// <summary>
    /// Interaction logic for MainPage
    /// </summary>
    [DependentView(RegionNames.FunctionRegion, typeof(NavigationMenu))]
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }
}
