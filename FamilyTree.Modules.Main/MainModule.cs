using FamilyTree.Core;
using FamilyTree.Modules.Main.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace FamilyTree.Modules.Main
{
    public class MainModule : IModule
    {
        private readonly RegionManager _regionManager;

        public MainModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, typeof(NavigationMenu));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}