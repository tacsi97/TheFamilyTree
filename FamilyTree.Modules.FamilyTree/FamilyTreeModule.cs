using FamilyTree.Core;
using FamilyTree.Modules.FamilyTree.Repository;
using FamilyTree.Modules.FamilyTree.ViewModels;
using FamilyTree.Modules.FamilyTree.Views;
using FamilyTree.Services.Repository;
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
            //_regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ListFamilyTreeView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IAsyncRepository<Business.FamilyTree>>(
                new FakeTreeRepository(
                    DatabaseInfo.Uri,
                    new Business.Token()
                    {
                        UserName = DatabaseInfo.UserName,
                        Code = DatabaseInfo.Password
                    }));

            containerRegistry.RegisterDialog<NewFamilyTreeView, NewFamilyTreeViewModel>(DialogNames.NewTreeDialog);

            containerRegistry.RegisterForNavigation<ModifyFamilyTreeView, ModifyFamilyTreeViewModel>();
            containerRegistry.RegisterForNavigation<BackFamilyTreeView, BackFamilyTreeViewViewModel>();
            containerRegistry.RegisterForNavigation<NewFamilyTreeView, NewFamilyTreeViewModel>();
            containerRegistry.RegisterForNavigation<ListFamilyTreeView, ListFamilyTreeViewModel>();
            containerRegistry.RegisterForNavigation<FunctionFamilyTreeView, FunctionFamilyTreeViewModel>();
        }
    }
}