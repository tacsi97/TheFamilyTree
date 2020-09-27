using System;
using Xunit;
using Moq;
using FamilyTree.Services.Repository.Interfaces;
using FamilyTree.Modules.Person.ViewModels;
using Prism.Mvvm;

namespace FamilyTree.Modules.Person.Tests.ViewModels
{
    public class NewPersonDialogViewModelFixture
    {
        Mock<IAsyncRepository<Business.Person>> _repository;

        public NewPersonDialogViewModelFixture()
        {
            _repository = new Mock<IAsyncRepository<Business.Person>>();
        }

        [Fact]
        public void CanExecuteWithFirstNameNull()
        {
            var vm = new NewPersonDialogViewModel(_repository.Object);

            vm.LastName = "asd";
            vm.DateOfBirth = DateTime.Parse("2010-10-10");

            Assert.False(vm.CanExecuteSubmit());
        }

        [Fact]
        public void CanExecuteWithLastNameNull()
        {
            var vm = new NewPersonDialogViewModel(_repository.Object);

            vm.FirstName = "asd";
            vm.DateOfBirth = DateTime.Parse("2010-10-10");

            Assert.False(vm.CanExecuteSubmit());
        }

        [Fact]
        public void CanExecuteWithDateOfBirthNull()
        {
            var vm = new NewPersonDialogViewModel(_repository.Object);

            vm.LastName = "asd";
            vm.FirstName = "asd";
            vm.DateOfBirth = DateTime.MinValue;
            Assert.False(vm.CanExecuteSubmit());
        }

        [Fact]
        public void CanExecuteReturnTrue()
        {
            var vm = new NewPersonDialogViewModel(_repository.Object);

            vm.LastName = "asd";
            vm.FirstName = "asd";
            vm.DateOfBirth = DateTime.Parse("2010-10-10");

            Assert.True(vm.CanExecuteSubmit());
        }

    }
}
