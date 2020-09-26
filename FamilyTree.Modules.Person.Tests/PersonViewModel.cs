using System;
using Xunit;
using Moq;
using FamilyTree.Services.Repository.Interfaces;
using FamilyTree.Modules.Person.ViewModels;

namespace FamilyTree.Modules.Person.Tests
{
    public class PersonViewModel
    {
        Mock<IAsyncRepository<Business.Person>> _repository;

        public PersonViewModel()
        {
            _repository = new Mock<IAsyncRepository<Business.Person>>();
        }

        [Fact]
        public void ViewModel()
        {
            var vm = new NewPersonDialogViewModel(_repository.Object);

            
        }
    }
}
