using FamilyTree.Core;
using FamilyTree.Core.Commands;
using FamilyTree.Modules.FamilyTree.Core;
using FamilyTree.Modules.FamilyTree.PubSubEvents;
using FamilyTree.Services.Repository.Interfaces;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.Modules.FamilyTree.ViewModels
{
    public class FamilyTreeFunctionViewModel : BindableBase
    {
        #region Fields

        private readonly IAsyncGraphRepository<Business.FamilyTree> _repository;
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        #endregion

        #region Commands

        private DelegateCommand _createTreeCommand;
        public DelegateCommand CreateTreeCommand =>
            _createTreeCommand ?? (_createTreeCommand = new DelegateCommand(ExecuteCreateTreeCommand, CanExecuteCreateTreeCommand));

        private DelegateCommand<Business.FamilyTree> _modifyTreeCommand;
        public DelegateCommand<Business.FamilyTree> ModifyTreeCommand =>
            _modifyTreeCommand ?? (_modifyTreeCommand = new DelegateCommand<Business.FamilyTree>(ExecuteModifyTreeCommand, CanExecuteModifyTreeCommand));

        private IAsyncGenericCommand<Business.FamilyTree> _deleteTreeCommand;
        public IAsyncGenericCommand<Business.FamilyTree> DeleteTreeCommand =>
            _deleteTreeCommand ?? (_deleteTreeCommand = new AsyncGenericCommand<Business.FamilyTree>(ExecuteDeleteTreeCommand, CanExecuteDeleteTreeCommand));

        private DelegateCommand _navigateCommand;
        public DelegateCommand NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand(ExecuteNavigateCommand, CanExecuteNavigateCommand));

        #endregion

        #region Properties

        private Business.FamilyTree _selectedTree;
        public Business.FamilyTree SelectedTree
        {
            get { return _selectedTree; }
            set
            {
                SetProperty(ref _selectedTree, value);
                ModifyTreeCommand.RaiseCanExecuteChanged();
                DeleteTreeCommand.RaiseCanExecuteChanged();
                NavigateCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        public FamilyTreeFunctionViewModel(IAsyncGraphRepository<Business.FamilyTree> repository, IDialogService dialogService, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _repository = repository;
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _eventAggregator.GetEvent<SelectedTreeChanged>().Subscribe(ChangeSelectedTree);
        }

        #region CreateTreeCommand

        public void ExecuteCreateTreeCommand()
        {
            _dialogService.ShowDialog(DialogNames.NewTreeDialog, null, null);
        }

        public bool CanExecuteCreateTreeCommand() => true;

        #endregion

        #region ModifyTreeCommand

        public void ExecuteModifyTreeCommand(Business.FamilyTree familyTree)
        {
            var parameters = new DialogParameters();

            parameters.Add(DialogParameterNames.Tree, familyTree);

            _dialogService.ShowDialog(DialogNames.ModifyTreeDialog, parameters, r =>
            {

            });
        }

        public bool CanExecuteModifyTreeCommand(Business.FamilyTree familyTree) => familyTree != null;

        #endregion

        #region DeleteTreeCommand

        public async Task ExecuteDeleteTreeCommand()
        {
            await _repository.DeleteAsync(SelectedTree.ID);
        }

        public bool CanExecuteDeleteTreeCommand() => SelectedTree != null;

        #endregion

        #region NavigateCommand

        public void ExecuteNavigateCommand()
        {
            var navParams = new NavigationParameters();
            navParams.Add("SelectedTree", SelectedTree);

            _regionManager.RequestNavigate(RegionNames.ContentRegion, "PeopleListView", navParams);
        }

        public bool CanExecuteNavigateCommand()
        {
            return SelectedTree != null;
        }

        #endregion

        public void ChangeSelectedTree(Business.FamilyTree familyTree)
        {
            SelectedTree = familyTree;
        }
    }
}
