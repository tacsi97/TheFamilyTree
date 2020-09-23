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
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAsyncRepository<Business.FamilyTree>, FakeRepository>();

            containerRegistry.RegisterDialog<NewTreeDialog, NewTreeDialogViewModel>(DialogNames.NewTreeDialog);
        }
    }
}