using FamilyTree.Modules.TreeDrawer.ViewModels;
using FamilyTree.Modules.TreeDrawer.Views;
using FamilyTree.Services.PersonConnector.Interfaces;
using FamilyTree.Services.TreeDrawer.Interfaces;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace FamilyTree.Modules.TreeDrawer
{
    public class TreeDrawerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // navigation view
            containerRegistry.RegisterForNavigation<ParentTreeView, ParentTreeViewModel>();

            containerRegistry.Register<ITreeDrawer, Services.TreeDrawer.TreeDrawer>();
            containerRegistry.Register<IPersonConnector, Services.PersonConnector.PersonConnector>();
        }
    }
}