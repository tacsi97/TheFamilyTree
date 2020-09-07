using Prism.Ioc;
using FamilyTree.Views;
using System.Windows;
using Prism.Modularity;
using FamilyTree.Modules.Main;
using FamilyTree.Modules.ModuleName;
using FamilyTree.Services.Interfaces;
using FamilyTree.Services;

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
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //moduleCatalog.AddModule<ModuleNameModule>();
            moduleCatalog.AddModule<MainModule>();
        }
    }
}
