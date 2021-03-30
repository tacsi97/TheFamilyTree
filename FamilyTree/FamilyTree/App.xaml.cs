using Prism.Ioc;
using FamilyTree.Views;
using System.Windows;
using Prism.Modularity;
using FamilyTree.Modules.Main;
using FamilyTree.Modules.ModuleName;
using FamilyTree.Services.Interfaces;
using FamilyTree.Services;
using FamilyTree.Core.ApplicationCommands;
using Prism.Regions;
using FamilyTree.Core.Attributes;
using FamilyTree.Core.Behaviors;
using FamilyTree.Modules.Person;
using FamilyTree.Modules.FamilyTree;
using FamilyTree.Business;
using FamilyTree.Modules.TreeDrawer;
using System.Windows.Controls;
using System.Windows.Media;
using FamilyTree.Core.Adapters;

namespace FamilyTree
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //singletons
            containerRegistry.RegisterSingleton<IApplicationCommand, ApplicationCommand>();

            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //moduleCatalog.AddModule<ModuleNameModule>();
            moduleCatalog.AddModule<MainModule>();
            moduleCatalog.AddModule<PersonModule>();
            moduleCatalog.AddModule<FamilyTreeModule>();
            moduleCatalog.AddModule<TreeDrawerModule>();
        }

        protected override void ConfigureDefaultRegionBehaviors(IRegionBehaviorFactory regionBehaviors)
        {
            base.ConfigureDefaultRegionBehaviors(regionBehaviors);

            regionBehaviors.AddIfMissing(DependentViewRegionBehavior.BehaviorKey, typeof(DependentViewRegionBehavior));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            button.Background = Brushes.Red;
        }

        private void Button_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var button = sender as Button;

            button.Background = Brushes.Black;
        }

        private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }
    }
}
