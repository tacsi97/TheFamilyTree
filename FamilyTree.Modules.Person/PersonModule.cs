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
            containerRegistry.RegisterForNavigation<PeopleListView, PeopleListViewModel>();
            containerRegistry.RegisterForNavigation<PeopleAllView, PeopleAllViewModel>();
            containerRegistry.RegisterForNavigation<PeopleParentView, PeopleParentViewModel>();

            containerRegistry.RegisterInstance<IAsyncRemoteRepository<Business.Person>>(
                new PersonGraphRepository(
                    DatabaseInfo.Uri,
                    DatabaseInfo.UserName,
                    DatabaseInfo.Password));
            containerRegistry.RegisterSingleton<IUpload, UploadNewPersonCommand>();

            containerRegistry.RegisterDialog<NewPersonDialog, NewPersonDialogViewModel>(PersonDialogNames.AddNewPersonDialog);
            containerRegistry.RegisterDialog<ModifyPersonView, ModifyPersonViewModel>(PersonDialogNames.ModifyPersonDialog);
            containerRegistry.RegisterDialog<PersonInfoView, PersonInfoViewModel>(PersonDialogNames.ShowPersonDialog);
            containerRegistry.RegisterDialog<DeletePersonView, DeletePersonViewModel>(PersonDialogNames.DeletePersonDialog);
        }
    }
}