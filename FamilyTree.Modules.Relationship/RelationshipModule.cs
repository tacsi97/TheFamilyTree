using FamilyTree.Core;
using FamilyTree.Core.ApplicationCommands;
using FamilyTree.Modules.Relationship.Repository;
using FamilyTree.Modules.Relationship.ViewModels;
using FamilyTree.Modules.Relationship.Views;
using FamilyTree.Services.Repository.Interfaces;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace FamilyTree.Modules.Relationship
{
    public class RelationshipModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public RelationshipModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.RelationshipRegion, typeof(SetRelationShipView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IAsyncRemoteRepository<Business.Relationship>>(
                new RelationshipGraphRepository(
                    DatabaseInfo.Uri,
                    DatabaseInfo.UserName,
                    DatabaseInfo.Password));
            // Singleton
            containerRegistry.RegisterSingleton<IUpload, UploadNewPersonCommand>();

            

            containerRegistry.RegisterForNavigation<ListRelationshipView, ListRelationshipViewModel>();
            containerRegistry.RegisterForNavigation<RelationshipFunctions, RelationshipFunctionsViewModel>();

            containerRegistry.RegisterDialog<DeleteRelationshipView, DeleteRelationshipViewModel>(Core.DialogNames.DeleteRelationshipDialog);
            containerRegistry.RegisterDialog<EditRelationshipView, EditRelationshipViewModel>(Core.DialogNames.EditRelationshipDialog);
        }
    }
}