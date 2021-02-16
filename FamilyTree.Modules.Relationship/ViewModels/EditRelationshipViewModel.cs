using FamilyTree.Core;
using FamilyTree.Core.Commands;
using FamilyTree.Modules.Relationship.Core;
using FamilyTree.Services.Repository.Interfaces;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FamilyTree.Modules.Relationship.ViewModels
{
    public class EditRelationshipViewModel : BindableBase, IDialogAware
    {
        #region Fields

        private readonly IAsyncRepository<Business.Relationship> _repository;

        #endregion

        #region Properties
        private string _selectedType;
        public string SelectedType
        {
            get { return _selectedType; }
            set
            {
                SetProperty(ref _selectedType, value);
                SelectedRelationship.RelationType = value;
                SaveCommand.RaiseCanExecuteChanged(this, EventArgs.Empty);
            }
        }

        private Business.Relationship _selectedRelationship;
        public Business.Relationship SelectedRelationship
        {
            get { return _selectedRelationship; }
            set { SetProperty(ref _selectedRelationship, value); }
        }

        private DateTime _from;
        public DateTime From
        {
            get
            {
                return _from;
            }
            set
            {
                SetProperty(ref _from, value);
                SelectedRelationship.From = value;
                SaveCommand.RaiseCanExecuteChanged(this, EventArgs.Empty);
            }
        }

        private DateTime _to;
        public DateTime To
        {
            get
            {
                return _to;
            }
            set
            {
                SetProperty(ref _to, value);
                SelectedRelationship.To = value;
                SaveCommand.RaiseCanExecuteChanged(this, EventArgs.Empty);
            }
        }

        private string _relationType;
        public string RelationType
        {
            get
            {
                return _relationType;
            }
            set
            {
                SetProperty(ref _relationType, value);
                SelectedRelationship.RelationType = value;
                SaveCommand.RaiseCanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public string Title => "Módosít";

        public ObservableCollection<string> Types { get; set; }

        #endregion

        #region Events

        public event Action<IDialogResult> RequestClose;

        #endregion

        #region Commands

        private AsyncCommand _saveCommand;
        public AsyncCommand SaveCommand
        {
            get { return _saveCommand; }
            set { SetProperty(ref _saveCommand, value); }
        }

        #endregion

        public EditRelationshipViewModel(IAsyncRepository<Business.Relationship> repository)
        {
            _repository = repository;

            SaveCommand = new AsyncCommand(ExecuteSaveCommand, CanExecuteSaveCommand);

            Types = new ObservableCollection<string>();
            Types.Add(TypeNames.Parent);
            Types.Add(TypeNames.Partner);
            Types.Add(TypeNames.Child);
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            SelectedRelationship = parameters.GetValue<Business.Relationship>("SelectedRelationship");

            From = SelectedRelationship.From;
            To = SelectedRelationship.To;
            SelectedType = SelectedRelationship.RelationType;
        }

        public async Task ExecuteSaveCommand()
        {
            try
            {
                await _repository.ModifyAsync(SelectedRelationship);

                RequestClose(new DialogResult(ButtonResult.OK));
            }
            catch (Exception)
            {
                // TODO: do something
            }
        }

        public bool CanExecuteSaveCommand()
        {
            return SelectedRelationship != null
                && SelectedRelationship.From != null
                && SelectedRelationship.From != DateTime.MinValue
                && SelectedRelationship.To != null
                && SelectedRelationship.To != DateTime.MinValue
                && !string.IsNullOrEmpty(SelectedRelationship.RelationType);
        }
    }
}
