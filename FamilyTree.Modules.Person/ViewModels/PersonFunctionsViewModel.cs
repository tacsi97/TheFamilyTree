using FamilyTree.Business;
using FamilyTree.Core;
using FamilyTree.Core.ApplicationCommands;
using FamilyTree.Core.PubSubEvents;
using FamilyTree.Modules.Person.Core;
using FamilyTree.Modules.Person.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FamilyTree.Modules.Person.ViewModels
{
    public class PersonFunctionsViewModel : BindableBase
    {
        #region Fields

        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        private ViewType _viewType;

        #endregion

        #region Properties

        private Business.FamilyTree _familyTree;
        public Business.FamilyTree FamilyTree
        {
            get { return _familyTree; }
            set
            {
                SetProperty(ref _familyTree, value);
                NavigateTreeViewCommand.RaiseCanExecuteChanged();
            }
        }

        private Business.Person _selectedPerson;
        public Business.Person SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                SetProperty(ref _selectedPerson, value);
                NewFatherCommand.RaiseCanExecuteChanged();
                NewMotherCommand.RaiseCanExecuteChanged();
                NewChildCommand.RaiseCanExecuteChanged();
                NewPairCommand.RaiseCanExecuteChanged();
                ModifyPersonCommand.RaiseCanExecuteChanged();
                ShowPersonCommand.RaiseCanExecuteChanged();
                DeletePersonCommand.RaiseCanExecuteChanged();
                NavigateTreeViewCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Commands

        // TODO: A legtöbb command más viewmodelben kellene hogy szerepeljen
        private DelegateCommand _modifyPersonCommand;
        public DelegateCommand ModifyPersonCommand =>
            _modifyPersonCommand ?? (_modifyPersonCommand = new DelegateCommand(ExecuteModifyPersonCommand, CanExecuteModifyPersonCommand));

        private DelegateCommand _showPersonCommand;
        public DelegateCommand ShowPersonCommand =>
            _showPersonCommand ?? (_showPersonCommand = new DelegateCommand(ExecuteShowPersonCommand, CanExecuteShowPersonCommand));

        private DelegateCommand _deletePersonCommand;
        public DelegateCommand DeletePersonCommand =>
            _deletePersonCommand ?? (_deletePersonCommand = new DelegateCommand(ExecuteDeletePersonCommand, CanExecuteDeletePersonCommand));

        private DelegateCommand _navigateTreeViewCommand;
        public DelegateCommand NavigateTreeViewCommand =>
            _navigateTreeViewCommand ?? (_navigateTreeViewCommand = new DelegateCommand(ExecuteNavigateTreeViewCommand, CanExecuteNavigateTreeViewCommand));

        private DelegateCommand _newFatherCommand;
        public DelegateCommand NewFatherCommand =>
            _newFatherCommand ?? (_newFatherCommand = new DelegateCommand(ExecuteNewFatherNavigateCommand, CanExecuteNewFatherNavigateCommand));

        private DelegateCommand _newMotherCommand;
        public DelegateCommand NewMotherCommand =>
            _newMotherCommand ?? (_newMotherCommand = new DelegateCommand(ExecuteNewMotherNavigateCommand, CanExecuteNewMotherNavigateCommand));

        private DelegateCommand _newPairCommand;
        public DelegateCommand NewPairCommand =>
            _newPairCommand ?? (_newPairCommand = new DelegateCommand(ExecuteNewPairCommand, CanExecuteNewPairCommand));

        private DelegateCommand _newChildCommand;
        public DelegateCommand NewChildCommand =>
            _newChildCommand ?? (_newChildCommand = new DelegateCommand(ExecuteNewChildNavigateCommand, CanExecuteNewChildNavigateCommand));

        #endregion

        public PersonFunctionsViewModel(IDialogService dialogService, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;

            _eventAggregator.GetEvent<SelectedPersonChangedEvent>().Subscribe(SetPerson);
            _eventAggregator.GetEvent<SelectedTreeChangedEvent>().Subscribe(SetTree);
            _eventAggregator.GetEvent<SelectedViewTypeChangedEvent>().Subscribe(SetViewType);
        }

        #region ModifyPersonFunctions

        public void ExecuteModifyPersonCommand()
        {
            var parameters = new NavigationParameters();
            parameters.Add(NavParamNames.Person, SelectedPerson);

            _regionManager.RequestNavigate(RegionNames.ContentRegion, "ModifyPersonView", parameters);
        }

        public bool CanExecuteModifyPersonCommand()
        {
            if (SelectedPerson == null)
                return false;

            return true;
        }

        #endregion

        #region ShowPersonFunctions

        public void ExecuteShowPersonCommand()
        {
            var parameters = new NavigationParameters();
            parameters.Add(NavParamNames.Person, SelectedPerson);

            _regionManager.RequestNavigate(RegionNames.ContentRegion, "InfoPersonView", parameters);
        }

        public bool CanExecuteShowPersonCommand()
        {
            if (SelectedPerson == null)
                return false;

            return true;
        }

        #endregion

        #region DeletePersonFunctions

        void ExecuteDeletePersonCommand()
        {
            var parameters = new NavigationParameters();
            parameters.Add(NavParamNames.Person, SelectedPerson);

            _regionManager.RequestNavigate(RegionNames.ContentRegion, "DeletePersonView", parameters);
        }

        bool CanExecuteDeletePersonCommand()
        {
            if (SelectedPerson == null)
                return false;

            return true;
        }

        #endregion

        #region TreeViewFunctions

        public void ExecuteNavigateTreeViewCommand()
        {
            var navParams = new NavigationParameters();
            navParams.Add(NavParamNames.Person, SelectedPerson);

            switch (_viewType)
            {
                case ViewType.ListView:
                    _viewType = ViewType.ParentView;
                    navParams.Add(NavParamNames.ViewType, _viewType);
                    _regionManager.RequestNavigate(RegionNames.ContentRegion, "ParentTreePersonView", navParams);
                    break;
                case ViewType.ParentView:
                    _viewType = ViewType.ChildrenView;
                    navParams.Add(NavParamNames.ViewType, _viewType);
                    _regionManager.RequestNavigate(RegionNames.ContentRegion, "ChildTreePersonView", navParams);
                    break;
                case ViewType.ChildrenView:
                    _viewType = ViewType.ListView;
                    navParams.Add(NavParamNames.ViewType, _viewType);
                    _regionManager.RequestNavigate(RegionNames.ContentRegion, "ListPersonView", navParams);
                    break;
                default:
                    break;
            }
        }

        public bool CanExecuteNavigateTreeViewCommand()
        {
            return SelectedPerson != null;
        }

        #endregion

        #region NewFatherNavigateCommand

        public void ExecuteNewFatherNavigateCommand()
        {
            var navParams = new NavigationParameters();
            navParams.Add(NavParamNames.Person, SelectedPerson);

            _regionManager.RequestNavigate(RegionNames.ContentRegion, "CreateFatherView", navParams);
        }

        public bool CanExecuteNewFatherNavigateCommand()
        {
            return SelectedPerson != null && SelectedPerson.Father == null;
        }

        #endregion

        #region NewMotherNavigateCommand

        public void ExecuteNewMotherNavigateCommand()
        {
            var navParams = new NavigationParameters();
            navParams.Add(NavParamNames.Person, SelectedPerson);

            _regionManager.RequestNavigate(RegionNames.ContentRegion, "CreateMotherView", navParams);
        }

        public bool CanExecuteNewMotherNavigateCommand()
        {
            return SelectedPerson != null && SelectedPerson.Mother == null;
        }

        #endregion

        #region NewChildNavigateCommand

        public void ExecuteNewChildNavigateCommand()
        {
            var navParams = new NavigationParameters();

            if (SelectedPerson.Gender == GenderType.Male)
            {
                navParams.Add(NavParamNames.Father, SelectedPerson);
                navParams.Add(NavParamNames.Mother, SelectedPerson.Partner);
            }
            else
            {
                navParams.Add(NavParamNames.Mother, SelectedPerson);
                navParams.Add(NavParamNames.Father, SelectedPerson.Partner);
            }

            _regionManager.RequestNavigate(RegionNames.ContentRegion, "CreateChildView", navParams);
        }

        public bool CanExecuteNewChildNavigateCommand()
        {
            return SelectedPerson != null;
        }

        #endregion

        #region NewPairNavigateCommand

        public void ExecuteNewPairCommand()
        {
            var navParams = new NavigationParameters();
            navParams.Add(NavParamNames.Person, SelectedPerson);
            navParams.Add("NewPersonRole", "Pair");

            _regionManager.RequestNavigate(RegionNames.ContentRegion, "CreatePersonView", navParams);
        }

        public bool CanExecuteNewPairCommand()
        {
            return SelectedPerson != null && SelectedPerson.Partner == null;
        }

        #endregion

        public void SetPerson(Business.Person person)
        {
            SelectedPerson = person;
        }

        public void SetTree(Business.FamilyTree tree)
        {
            FamilyTree = tree;
        }

        public void SetViewType(Business.ViewType viewType)
        {
            _viewType = viewType;
        }
    }

}
