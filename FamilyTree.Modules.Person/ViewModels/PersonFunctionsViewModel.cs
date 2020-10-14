using FamilyTree.Core.PubSubEvents;
using FamilyTree.Modules.Person.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
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
            }
        }

        #endregion

        #region Commands

        private DelegateCommand _addPersonCommand;

        public DelegateCommand AddPersonCommand =>
            _addPersonCommand ?? (_addPersonCommand = new DelegateCommand(ExecuteAddPersonCommand, CanExecuteAddPersonCommand));

        #endregion

        public PersonFunctionsViewModel(IDialogService dialogService, IEventAggregator eventAggregator)
        {
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<SelectedPersonChangedEvent>().Subscribe(SetPerson);
        }

        public void ExecuteAddPersonCommand()
        {
            var parameters = new DialogParameters();
            parameters.Add("SelectedPerson", SelectedPerson);

            // TODO: Nem itt végzi el a műveletet, hanem a dialognál, hogy tudjon jelezni időben, ha valami nem sikerült, majd ha végzett visszaadja az embert, amit hozzáadunk a listához, így nem kell mindenkit lekérdezni, plusz az observable collection is működik.
            _dialogService.Show(PersonDialogNames.AddNewPersonDialog, parameters, r =>
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

        public void SetPerson(Business.Person person)
        {
            SelectedPerson = person;
        }
    }
}
