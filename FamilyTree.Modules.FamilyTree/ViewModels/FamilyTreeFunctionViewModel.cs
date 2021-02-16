using FamilyTree.Core;
using FamilyTree.Core.Commands;
using FamilyTree.Modules.FamilyTree.Core;
using FamilyTree.Modules.FamilyTree.PubSubEvents;
using FamilyTree.Modules.FamilyTree.Views;
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

        private readonly IAsyncRepository<Business.FamilyTree> _repository;
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        #endregion

        #region Commands

        private DelegateCommand<string> _newFamilyTreeNavigateCommand;
        public DelegateCommand<string> NewFamilyTreeNavigateCommand =>
            _newFamilyTreeNavigateCommand ?? (_newFamilyTreeNavigateCommand = new DelegateCommand<string>(ExecuteNewFamilyTreeNavigateCommand, CanExecuteNewFamilyTreeNavigateCommand));

        private DelegateCommand<string> _modifyFamilyTreeNavigateCommand;
        public DelegateCommand<string> ModifyFamilyTreeNavigateCommand =>
            _modifyFamilyTreeNavigateCommand ?? (_modifyFamilyTreeNavigateCommand = new DelegateCommand<string>(ExecuteModifyFamilyTreeNavigateCommand, CanExecuteModifyFamilyTreeNavigateCommand));

        private DelegateCommand<string> _deleteFamilyTreeNavigateCommand;
        public DelegateCommand<string> DeleteFamilyTreeNavigateCommand =>
            _deleteFamilyTreeNavigateCommand ?? (_deleteFamilyTreeNavigateCommand = new DelegateCommand<string>(ExecuteDeleteFamilyTreeNavigateCommand, CanExecuteDeleteFamilyTreeNavigateCommand));

        private DelegateCommand<string> _showPeopleNavigateCommand;
        public DelegateCommand<string> ShowPeopleNavigateCommand =>
            _showPeopleNavigateCommand ?? (_showPeopleNavigateCommand = new DelegateCommand<string>(ExecuteShowPeopleNavigateCommand, CanExecuteShowPeopleNavigateCommand));

        private DelegateCommand _createTreeCommand;
        public DelegateCommand CreateTreeCommand =>
            _createTreeCommand ?? (_createTreeCommand = new DelegateCommand(ExecuteCreateTreeCommand, CanExecuteCreateTreeCommand));

        private DelegateCommand<Business.FamilyTree> _modifyTreeCommand;
        public DelegateCommand<Business.FamilyTree> ModifyTreeCommand =>
            _modifyTreeCommand ?? (_modifyTreeCommand = new DelegateCommand<Business.FamilyTree>(ExecuteModifyTreeCommand, CanExecuteModifyTreeCommand));

        private IAsyncGenericCommand<Business.FamilyTree> _deleteTreeCommand;
        public IAsyncGenericCommand<Business.FamilyTree> DeleteTreeCommand =>
            _deleteTreeCommand ?? (_deleteTreeCommand = new AsyncGenericCommand<Business.FamilyTree>(ExecuteDeleteTreeCommand, CanExecuteDeleteTreeCommand));

        #endregion

        #region Properties

        private Business.FamilyTree _selectedTree;
        public Business.FamilyTree SelectedTree
        {
            get { return _selectedTree; }
            set
            {
                SetProperty(ref _selectedTree, value);
                ModifyFamilyTreeNavigateCommand.RaiseCanExecuteChanged();
                DeleteFamilyTreeNavigateCommand.RaiseCanExecuteChanged();
                ShowPeopleNavigateCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        public FamilyTreeFunctionViewModel(IAsyncRepository<Business.FamilyTree> repository, IDialogService dialogService, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _repository = repository;
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _eventAggregator.GetEvent<SelectedTreeChanged>().Subscribe(ChangeSelectedTree);
            SelectedTree = new Business.FamilyTree() { Name = "Teszt" };
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

        public void ExecuteNavigateCommand(object param)
        {
            var navParams = new NavigationParameters();

            _regionManager.RequestNavigate(RegionNames.ContentRegion, "PeopleListView", navParams);
        }

        public bool CanExecuteNavigateCommand(object param)
        {
            return SelectedTree != null;
        }

        #endregion

        #region NewFamilyTreeCommand

        public void ExecuteNewFamilyTreeNavigateCommand(string navigationPath)
        {
            var navParams = new NavigationParameters();
            navParams.Add(NavParamNames.Tree, new Business.FamilyTree());

            _regionManager.RequestNavigate(RegionNames.ContentRegion, navigationPath, navParams);
        }

        public bool CanExecuteNewFamilyTreeNavigateCommand(string navigationPath)
        {
            return navigationPath != null;
        }

        #endregion

        #region ModifyFamilyTreeCommand
        // TODO: valahogy ezeket egységesíteni
        public void ExecuteModifyFamilyTreeNavigateCommand(string navigationPath)
        {
            var navParams = new NavigationParameters();
            navParams.Add(NavParamNames.Tree, SelectedTree);

            _regionManager.RequestNavigate(RegionNames.ContentRegion, navigationPath, navParams);
        }

        public bool CanExecuteModifyFamilyTreeNavigateCommand(string navigationPath)
        {
            return navigationPath != null && SelectedTree != null;
        }

        #endregion

        #region DeleteFamilyTreeCommand

        public void ExecuteDeleteFamilyTreeNavigateCommand(string navigationPath)
        {
            var navParams = new NavigationParameters();
            navParams.Add(NavParamNames.Tree, SelectedTree);

            _regionManager.RequestNavigate(RegionNames.ContentRegion, navigationPath, navParams);
        }

        public bool CanExecuteDeleteFamilyTreeNavigateCommand(string navigationPath)
        {
            return navigationPath != null && SelectedTree != null;
        }

        #endregion

        #region ShowPeopleCommand

        public void ExecuteShowPeopleNavigateCommand(string navigationPath)
        {
            var navParams = new NavigationParameters();
            navParams.Add(NavParamNames.Tree, SelectedTree);

            _regionManager.RequestNavigate(RegionNames.ContentRegion, navigationPath, navParams);
        }

        public bool CanExecuteShowPeopleNavigateCommand(string navigationPath)
        {
            return navigationPath != null && SelectedTree != null;
        }

        #endregion

        public void ChangeSelectedTree(Business.FamilyTree familyTree)
        {
            SelectedTree = familyTree;
        }
    }
}
