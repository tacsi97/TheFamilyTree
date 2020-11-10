using FamilyTree.Modules.FamilyTree.Repository;
using FamilyTree.Modules.FamilyTree.ViewModels;
using FamilyTree.Services.Repository.Interfaces;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Text;
using Xunit;

namespace FamilyTree.Modules.FamilyTree.Tests.ViewModels
{
    public class NewTreeDialogViewModelFixture
    {
        Mock<IAsyncRepository<Business.FamilyTree>> _repository;

        public NewTreeDialogViewModelFixture()
        {
            _repository = new Mock<IAsyncRepository<Business.FamilyTree>>();
        }

        [Fact]
        public void CanExecuteWithFamilyTreeNameNull()
        {
            var vm = new NewTreeDialogViewModel(_repository.Object);

            vm.FamilyTreeName = null;

            Assert.False(vm.SubmitCanExecute());
        }

        [Fact]
        public void CanExecuteWithFamilyTreeNameSet()
        {
            var vm = new NewTreeDialogViewModel(_repository.Object);

            vm.FamilyTreeName = "Család";

            Assert.True(vm.SubmitCanExecute());
        }

    }
}
