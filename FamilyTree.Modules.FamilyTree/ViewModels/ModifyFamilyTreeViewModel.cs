using FamilyTree.Core;
using FamilyTree.Core.Commands;
using FamilyTree.Modules.FamilyTree.Core;
using FamilyTree.Services.Repository.Interfaces;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.Modules.FamilyTree.ViewModels
{
    public class ModifyFamilyTreeViewModel : BindableBase, INavigationAware
    {
        // TODO: instead of passing the whole object, I should pass only the ID and the Name
        private readonly IAsyncRepository<Business.FamilyTree> _repository;
        private readonly IRegionManager _regionManager;

        public Business.FamilyTree FamilyTree { get; set; }

        private Business.FamilyTree _selectedTree;
        public Business.FamilyTree SelectedTree
        {
            get { return _selectedTree; }
            set
            {
                SetProperty(ref _selectedTree, value);
                ModifyCommand.RaiseCanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public IAsyncCommand ModifyCommand { get; set; }

        public ModifyFamilyTreeViewModel(IAsyncRepository<Business.FamilyTree> repository, IRegionManager regionManager)
        {
            _repository = repository;
            _regionManager = regionManager;
            ModifyCommand = new AsyncCommand(ExecuteModifyTreeCommand, CanExecuteModifyTreeCommand);
        }

        public async Task ExecuteModifyTreeCommand()
        {
            FamilyTree = new Business.FamilyTree()
            {
                Name = SelectedTree.Name,
                People = SelectedTree.People,
                ID = SelectedTree.ID
            };

            await _repository.ModifyAsync(FamilyTree);

            var navParams = new NavigationParameters();
            navParams.Add(NavParamNames.Tree, FamilyTree);

            _regionManager.RequestNavigate(RegionNames.ContentRegion, "ListFamilyTreeView", navParams);
        }

        /// <summary>
        /// Determines that, the command can be executed or not.
        /// </summary>
        /// <returns>If the <c>NewTreeDialogViewModel</c>'s FamilyTreeName property is null or empty, then it return false.</returns>
        public bool CanExecuteModifyTreeCommand() => SelectedTree != null;

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            SelectedTree = navigationContext.Parameters.GetValue<Business.FamilyTree>(NavParamNames.Tree);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            navigationContext.Parameters.Add(NavParamNames.Tree, FamilyTree);
        }
    }
}

