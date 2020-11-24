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
            containerRegistry.RegisterInstance<IAsyncGraphRepository<Business.FamilyTree>>(
                new FamilyTreeGraphRepository(
                    DatabaseInfo.Uri,
                    DatabaseInfo.UserName,
                    DatabaseInfo.Password));

            containerRegistry.RegisterDialog<NewFamilyTreeView, NewFamilyTreeViewModel>(DialogNames.NewTreeDialog);
            containerRegistry.RegisterDialog<ModifyTreeDialog, ModifyFamilyTreeViewModel>(DialogNames.ModifyTreeDialog);

            containerRegistry.RegisterForNavigation<FamilyTreeListView, FamilyTreeListViewModel>();
            containerRegistry.RegisterForNavigation<FamilyTreeFunctionView, FamilyTreeFunctionViewModel>();
        }
    }
}