using FamilyTree.Core;
using FamilyTree.Modules.Main.ViewModels;
using FamilyTree.Modules.Main.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace FamilyTree.Modules.Main
{
    public class MainModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public MainModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, typeof(NavigationMenu));
            _regionManager.RegisterViewWithRegion(RegionNames.FunctionRegion, typeof(FunctionView));
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(MainPage));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<FunctionView, FunctionViewViewModel>();
        }
    }
}