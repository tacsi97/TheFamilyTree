using FamilyTree.Core;
using FamilyTree.Modules.Person.Commands;
using FamilyTree.Services.Repository.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.Modules.Person.ViewModels
{
    public class PeopleListViewModel : BindableBase
    {
        private readonly IAsyncRepository<Business.Person> _repository;

        #region Properties

        public ObservableCollection<Business.Person> People { get; set; }

        #endregion

        #region Commands

        public GetPeopleCommand GetPeopleCommand { get; set; }

        #endregion

        public PeopleListViewModel(IAsyncRepository<Business.Person> repository)
        {
            _repository = repository;

            GetPeopleCommand = new GetPeopleCommand(this);

            People = new ObservableCollection<Business.Person>();
        }

        public async Task ExecuteGetPeopleCommand()
        {
            People.Clear();

            var people = await _repository.GetAllAsync(Uris.PersonURI);

            foreach (var person in people)
            {
                People.Add(person);
            }
        }
    }
}
