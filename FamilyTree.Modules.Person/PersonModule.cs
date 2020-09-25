using FamilyTree.Core;
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
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(PeopleListView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAsyncRepository<Business.Person>, FakePersonRepository>();

            containerRegistry.RegisterDialog<NewPersonDialog, NewPersonDialogViewModel>(DialogNames.NewPersonDialog);

            containerRegistry.RegisterForNavigation<PeopleListView, PeopleListViewModel>();
        }
    }
}