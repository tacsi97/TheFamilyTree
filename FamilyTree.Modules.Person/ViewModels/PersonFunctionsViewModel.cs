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

        #endregion

        #region Properties

        private Business.Person _selectedPerson;
        public Business.Person SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                SetProperty(ref _selectedPerson, value);
                AddPersonCommand.RaiseCanExecuteChanged();
                ModifyPersonCommand.RaiseCanExecuteChanged();
                ShowPersonCommand.RaiseCanExecuteChanged();
                DeletePersonCommand.RaiseCanExecuteChanged();
                NavigateCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Commands

        private DelegateCommand _addPersonCommand;
        public DelegateCommand AddPersonCommand =>
            _addPersonCommand ?? (_addPersonCommand = new DelegateCommand(ExecuteAddPersonCommand, CanExecuteAddPersonCommand));

        private DelegateCommand _modifyPersonCommand;
        public DelegateCommand ModifyPersonCommand =>
            _modifyPersonCommand ?? (_modifyPersonCommand = new DelegateCommand(ExecuteModifyPersonCommand, CanExecuteModifyPersonCommand));

        private DelegateCommand _showPersonCommand;
        public DelegateCommand ShowPersonCommand =>
            _showPersonCommand ?? (_showPersonCommand = new DelegateCommand(ExecuteShowPersonCommand, CanExecuteShowPersonCommand));

        private DelegateCommand _deletePersonCommand;
        public DelegateCommand DeletePersonCommand =>
            _deletePersonCommand ?? (_deletePersonCommand = new DelegateCommand(ExecuteDeletePersonCommand, CanExecuteDeletePersonCommand));

        private DelegateCommand _navigateCommand;
        public DelegateCommand NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand(ExecuteNavigateCommand, CanExecuteNavigateCommand));

        #endregion

        public PersonFunctionsViewModel(IDialogService dialogService, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;

            _eventAggregator.GetEvent<SelectedPersonChangedEvent>().Subscribe(SetPerson);
        }

        #region AddPersonFunctions

        public void ExecuteAddPersonCommand()
        {
            // TODO: ha már egyszer használva van a commandparameter, akkor azt kapja meg a függvény...
            var parameters = new DialogParameters();
            parameters.Add("SelectedPerson", SelectedPerson);

            // TODO: Nem itt végzi el a műveletet, hanem a dialognál, hogy tudjon jelezni időben, ha valami nem sikerült, majd ha végzett visszaadja az embert, amit hozzáadunk a listához, így nem kell mindenkit lekérdezni, plusz az observable collection is működik.
            _dialogService.ShowDialog(PersonDialogNames.AddNewPersonDialog, parameters, r =>
            {
                if (r.Result != ButtonResult.OK)
                    return;
            });
        }

        public bool CanExecuteAddPersonCommand()
        {
            if (SelectedPerson == null)
                return false;

            return true;
        }

        #endregion

        #region ModifyPersonFunctions

        public void ExecuteModifyPersonCommand()
        {
            var parameters = new DialogParameters();
            parameters.Add("SelectedPerson", SelectedPerson);

            _dialogService.ShowDialog(PersonDialogNames.ModifyPersonDialog, parameters, r =>
            {
                if (r.Result != ButtonResult.OK)
                    return;
            });
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
            var parameters = new DialogParameters();
            parameters.Add("SelectedPerson", SelectedPerson);

            // TODO: Nem itt végzi el a műveletet, hanem a dialognál, hogy tudjon jelezni időben, ha valami nem sikerült, majd ha végzett visszaadja az embert, amit hozzáadunk a listához, így nem kell mindenkit lekérdezni, plusz az observable collection is működik.
            _dialogService.ShowDialog(PersonDialogNames.ShowPersonDialog, parameters, r =>
            {
                if (r.Result != ButtonResult.OK)
                    return;
            });
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
            var parameters = new DialogParameters();
            parameters.Add("SelectedPerson", SelectedPerson);

            _dialogService.ShowDialog(PersonDialogNames.DeletePersonDialog, parameters, r =>
            {
                if (r.Result != ButtonResult.OK)
                    return;
            });
        }

        bool CanExecuteDeletePersonCommand()
        {
            if (SelectedPerson == null)
                return false;

            return true;
        }

        #endregion

        #region RelationshipFunctions

        public void ExecuteNavigateCommand()
        {
            //TODO: ez működik, a ListRelationshipView-hoz kell egy attribute
            var navParams = new NavigationParameters();
            navParams.Add("SelectedPerson", SelectedPerson);

            _regionManager.RequestNavigate(RegionNames.ContentRegion, "ListRelationshipView", navParams);
        }

        public bool CanExecuteNavigateCommand()
        {
            return SelectedPerson != null;
        }

        #endregion

        public void SetPerson(Business.Person person)
        {
            SelectedPerson = person;
        }
    }
}
