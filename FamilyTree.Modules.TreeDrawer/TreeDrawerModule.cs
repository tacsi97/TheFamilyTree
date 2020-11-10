using FamilyTree.Modules.TreeDrawer.ViewModels;
using FamilyTree.Modules.TreeDrawer.Views;
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
        }
    }
}