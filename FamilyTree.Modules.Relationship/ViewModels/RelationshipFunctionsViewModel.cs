using FamilyTree.Business;
using FamilyTree.Core.PubSubEvents;
using FamilyTree.Modules.Relationship.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FamilyTree.Modules.Relationship.ViewModels
{
    public class RelationshipFunctionsViewModel : BindableBase
    {
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;

        #region Properties

        private Business.Relationship _selectedRelationship;
        public Business.Relationship SelectedRelationship
        {
            get { return _selectedRelationship; }
            set
            {
                SetProperty(ref _selectedRelationship, value);
                EditRelationshipCommand.RaiseCanExecuteChanged();
                DeleteRelationshipCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Commands

        private DelegateCommand _editRelationshipCommand;
        public DelegateCommand EditRelationshipCommand =>
            _editRelationshipCommand ?? (_editRelationshipCommand = new DelegateCommand(ExecuteEditRelationshipCommand, CanExecuteEditRelationshipCommand));

        private DelegateCommand _deleteRelationshipCommand;
        public DelegateCommand DeleteRelationshipCommand =>
            _deleteRelationshipCommand ?? (_deleteRelationshipCommand = new DelegateCommand(ExecuteDeleteRelationshipCommand, CanExecuteDeleteRelationshipCommand));

        #endregion

        public RelationshipFunctionsViewModel(IDialogService dialogService, IEventAggregator eventAggregator)
        {
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;

            eventAggregator.GetEvent<SelectedRelationshipChangedEvent>().Subscribe(SetRelationship);
        }

        public void ExecuteEditRelationshipCommand()
        {
            var parameters = new DialogParameters();
            parameters.Add("SelectedRelationship", SelectedRelationship);

            _dialogService.ShowDialog(DialogNames.EditRelationshipDialog, parameters, r =>
            {
                if (r.Result != ButtonResult.OK) return;
            });
        }

        public bool CanExecuteEditRelationshipCommand()
        {
            return SelectedRelationship != null;
        }

        public void ExecuteDeleteRelationshipCommand()
        {
            var parameters = new DialogParameters();
            parameters.Add("SelectedRelationship", SelectedRelationship);

            _dialogService.ShowDialog(DialogNames.DeleteRelationshipDialog, parameters, r =>
            {
                if (r.Result != ButtonResult.OK) return;
            });
        }

        public bool CanExecuteDeleteRelationshipCommand()
        {
            return SelectedRelationship != null;
        }

        public void SetRelationship(Business.Relationship relationship)
        {
            SelectedRelationship = relationship;
        }
    }
}
