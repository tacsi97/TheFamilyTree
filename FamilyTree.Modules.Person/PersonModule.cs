using FamilyTree.Core;
using FamilyTree.Core.ApplicationCommands;
using FamilyTree.Modules.Person.Core;
using FamilyTree.Modules.Person.Repository;
using FamilyTree.Modules.Person.ViewModels;
using FamilyTree.Modules.Person.Views;
using FamilyTree.Services.Repository;
using FamilyTree.Services.Repository.Interfaces;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Net.Http;

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
            containerRegistry.RegisterForNavigation<CreatePersonView, CreatePersonViewModel>();
            containerRegistry.RegisterForNavigation<InfoPersonView, InfoPersonViewModel>();

            containerRegistry.RegisterInstance<IAsyncRepository<Business.Person>>(
                new FakePersonRepository(
                    DatabaseInfo.Uri,
                    new Business.Token()
                    {
                        UserName = DatabaseInfo.UserName,
                        Code = DatabaseInfo.Password
                    }));
            containerRegistry.RegisterSingleton<IUpload, UploadNewPersonCommand>();
        }
    }
}