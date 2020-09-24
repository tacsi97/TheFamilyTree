using FamilyTree.Core;
using FamilyTree.Modules.FamilyTree.Repository;
using FamilyTree.Modules.FamilyTree.ViewModels;
using FamilyTree.Modules.FamilyTree.Views;
using FamilyTree.Services.Repository.Interfaces;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace FamilyTree.Modules.FamilyTree
{
    public class FamilyTreeModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public FamilyTreeModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            //_regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(FamilyTreeListView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAsyncRepository<Business.FamilyTree>, FakeRepository>();

            containerRegistry.RegisterDialog<NewTreeDialog, NewTreeDialogViewModel>(DialogNames.NewTreeDialog);
        }
    }
}