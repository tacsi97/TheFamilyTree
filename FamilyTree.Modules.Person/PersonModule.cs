using FamilyTree.Modules.Person.Repository;
using FamilyTree.Services.Repository;
using FamilyTree.Services.Repository.Interfaces;
using Prism.Ioc;
using Prism.Modularity;
using System.Net.Http;

namespace FamilyTree.Modules.Person
{
    public class PersonModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAsyncRepository<Business.Person>, PersonRepository>();
        }
    }
}