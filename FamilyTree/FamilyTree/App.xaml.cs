using FamilyTree.Core.ApplicationCommands;
using FamilyTree.Core.Behaviors;
using FamilyTree.Modules.FamilyTree;
using FamilyTree.Modules.Main;
using FamilyTree.Modules.Person;
using FamilyTree.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Windows;

namespace FamilyTree
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            if (!System.IO.Directory.Exists("images"))
                System.IO.Directory.CreateDirectory("images");

            if (!System.IO.File.Exists("images/default-avatar.png"))
                System.IO.File.Copy("../../../default-avatar.png", "images/default-avatar.png");


            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //singletons
            containerRegistry.RegisterSingleton<IApplicationCommand, ApplicationCommand>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //moduleCatalog.AddModule<ModuleNameModule>();
            moduleCatalog.AddModule<MainModule>();
            moduleCatalog.AddModule<PersonModule>();
            moduleCatalog.AddModule<FamilyTreeModule>();
        }

        protected override void ConfigureDefaultRegionBehaviors(IRegionBehaviorFactory regionBehaviors)
        {
            base.ConfigureDefaultRegionBehaviors(regionBehaviors);

            regionBehaviors.AddIfMissing(DependentViewRegionBehavior.BehaviorKey, typeof(DependentViewRegionBehavior));
        }
    }
}
