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
using FamilyTree.Modules.Relationship;
using FamilyTree.Modules.TreeDrawer;

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
            moduleCatalog.AddModule<RelationshipModule>();
            moduleCatalog.AddModule<PersonModule>();
            moduleCatalog.AddModule<FamilyTreeModule>();
            moduleCatalog.AddModule<TreeDrawerModule>();
        }

        protected override void ConfigureDefaultRegionBehaviors(IRegionBehaviorFactory regionBehaviors)
        {
            base.ConfigureDefaultRegionBehaviors(regionBehaviors);

            regionBehaviors.AddIfMissing(DependentViewRegionBehavior.BehaviorKey, typeof(DependentViewRegionBehavior));
        }
    }
}
