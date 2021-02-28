using FamilyTree.Core;
using FamilyTree.Modules.FamilyTree.Commands;
using FamilyTree.Modules.FamilyTree.Core;
using FamilyTree.Services.Repository.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.Modules.FamilyTree.ViewModels
{
    public class DeleteFamilyTreeViewViewModel : BindableBase, INavigationAware
    {

        #region Fields

        private readonly IRegionManager _regionManager;
        private readonly IAsyncRepository<Business.FamilyTree> _repository;

        #endregion

        #region Properties

        private Business.FamilyTree _familyTree;
        public Business.FamilyTree FamilyTree
        {
            get { return _familyTree; }
            set { SetProperty(ref _familyTree, value); }
        }

        #endregion

        #region Command

        public DeleteCommand DeleteCommand { get; set; }

        #endregion

        public DeleteFamilyTreeViewViewModel(IRegionManager regionManager, IAsyncRepository<Business.FamilyTree> repository)
        {
            _regionManager = regionManager;
            _repository = repository;

            DeleteCommand = new DeleteCommand(this);
        }

        public async Task ExecuteDeleteCommand()
        {
            await _repository.DeleteAsync(FamilyTree.ID);

            _regionManager.RequestNavigate(RegionNames.ContentRegion, "ListFamilyTreeView");
        }

        public bool CanExecuteDeleteCommand()
        {
            return true;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            return;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            FamilyTree = navigationContext.Parameters.GetValue<Business.FamilyTree>(NavParamNames.Tree);
        }
    }
}
