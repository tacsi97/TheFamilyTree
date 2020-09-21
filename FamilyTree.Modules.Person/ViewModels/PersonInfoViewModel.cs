using FamilyTree.Services.Repository.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FamilyTree.Modules.Person.ViewModels
{
    public class PersonInfoViewModel : BindableBase
    {
        private readonly IAsyncRepository<Business.Person> _repository;

        private Business.Person _person;
        public Business.Person Person
        {
            get { return _person; }
            set { SetProperty(ref _person, value); }
        }

        public PersonInfoViewModel(IAsyncRepository<Business.Person> repository)
        {
            _repository = repository;
        }


    }
}
