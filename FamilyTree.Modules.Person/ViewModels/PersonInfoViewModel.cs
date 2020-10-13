using FamilyTree.Core.Mvvm;
using FamilyTree.Services.Repository.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FamilyTree.Modules.Person.ViewModels
{
    public class PersonInfoViewModel : BindableBase, IDialogAware
    {
        private readonly IAsyncRepository<Business.Person> _repository;

        private Business.Person _person;
        public Business.Person Person
        {
            get { return _person; }
            set { SetProperty(ref _person, value); }
        }

        public string Title => "Person info view";

        public event Action<IDialogResult> RequestClose;

        public PersonInfoViewModel(IAsyncRepository<Business.Person> repository)
        {
            _repository = repository;
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Person = parameters.GetValue<Business.Person>("person");
        }

        public void CloseDialog()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {

        }
    }
}
