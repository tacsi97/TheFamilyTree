using FamilyTree.Services.Repository.Interfaces;
using Moq;
using Xunit;

namespace FamilyTree.Modules.FamilyTree.Tests.ViewModels
{
    public class NewTreeDialogViewModelFixture
    {
        readonly Mock<IAsyncRepository<Business.FamilyTree>> _repository;

        public NewTreeDialogViewModelFixture()
        {
            _repository = new Mock<IAsyncRepository<Business.FamilyTree>>();
        }

        [Fact]
        public void CanExecuteWithFamilyTreeNameNull()
        {
            //var vm = new NewTreeDialogViewModel(_repository.Object);

            //vm.FamilyTreeName = null;

            //Assert.False(vm.SubmitCanExecute());
        }

        [Fact]
        public void CanExecuteWithFamilyTreeNameSet()
        {
            //var vm = new NewTreeDialogViewModel(_repository.Object);

            //vm.FamilyTreeName = "Család";

            //Assert.True(vm.SubmitCanExecute());
        }

    }
}
