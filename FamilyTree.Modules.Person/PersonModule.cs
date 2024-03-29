﻿using FamilyTree.Core;
using FamilyTree.Modules.Person.Repository;
using FamilyTree.Modules.Person.ViewModels;
using FamilyTree.Modules.Person.Views;
using FamilyTree.Services.Repository.Interfaces;
using FamilyTree.Services.TreeTravelsal;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace FamilyTree.Modules.Person
{
    public class PersonModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public PersonModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ListPersonView, ListPersonViewModel>();
            containerRegistry.RegisterForNavigation<DeletePersonView, DeletePersonViewModel>();
            containerRegistry.RegisterForNavigation<ModifyPersonView, ModifyPersonViewModel>();
            containerRegistry.RegisterForNavigation<InfoPersonView, InfoPersonViewModel>();
            containerRegistry.RegisterForNavigation<ParentTreePersonView, ParentTreePersonViewModel>();
            containerRegistry.RegisterForNavigation<ChildTreePersonView, ChildTreePersonViewModel>();
            containerRegistry.RegisterForNavigation<CreateChildView, CreateChildViewModel>();
            containerRegistry.RegisterForNavigation<CreatePartnerView, CreatePartnerViewModel>();
            containerRegistry.RegisterForNavigation<CreateParentsView, CreateParentsViewModel>();

            containerRegistry.Register<ChildrenTraverseBase, ChildrenTraverse>();
            containerRegistry.Register<ParentTraverseBase, ParentTraverse>();

            containerRegistry.RegisterInstance<IAsyncRepository<Business.Person>>(
                new LocalGraphRepository(
                    DatabaseInfo.Uri,
                    new Business.Token()
                    {
                        UserName = DatabaseInfo.UserName,
                        Code = DatabaseInfo.Password
                    }));
        }
    }
}