﻿using FamilyTree.Core;
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

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IAsyncRepository<Business.FamilyTree>>(
                new FamilyTreeGraphRepository(
                    DatabaseInfo.Uri,
                    new Business.Token()
                    {
                        UserName = DatabaseInfo.UserName,
                        Code = DatabaseInfo.Password
                    }));

            containerRegistry.RegisterForNavigation<ModifyFamilyTreeView, ModifyFamilyTreeViewModel>();
            containerRegistry.RegisterForNavigation<BackFamilyTreeView, BackFamilyTreeViewViewModel>();
            containerRegistry.RegisterForNavigation<CreateFamilyTreeView, CreateFamilyTreeViewModel>();
            containerRegistry.RegisterForNavigation<ListFamilyTreeView, ListFamilyTreeViewModel>();
            containerRegistry.RegisterForNavigation<FunctionFamilyTreeView, FunctionFamilyTreeViewModel>();
            containerRegistry.RegisterForNavigation<DeleteFamilyTreeView, DeleteFamilyTreeViewViewModel>();
        }
    }
}