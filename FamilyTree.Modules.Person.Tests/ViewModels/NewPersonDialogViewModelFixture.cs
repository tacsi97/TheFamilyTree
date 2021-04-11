using FamilyTree.Core.ApplicationCommands;
using FamilyTree.Core.Commands;
using FamilyTree.Services.Repository.Interfaces;
using Moq;
using Prism.Commands;
using Prism.Events;
using System;
using Xunit;

namespace FamilyTree.Modules.Person.Tests.ViewModels
{
    public class NewPersonDialogViewModelFixture
    {
        readonly Mock<IAsyncRepository<Business.Person>> _repository;
        readonly Mock<IEventAggregator> _eventAggregator;
        readonly Mock<IUpload> _iUpload;

        public NewPersonDialogViewModelFixture()
        {
            _repository = new Mock<IAsyncRepository<Business.Person>>();
            _eventAggregator = new Mock<IEventAggregator>();
            _iUpload = new Mock<IUpload>();
        }

        [Fact]
        public void delegatee()
        {

            var command = new DelegateCommand(
                () => { Console.WriteLine("Do something"); },
                () => false);

            command.RaiseCanExecuteChanged();
            command.Execute();
        }

        [Fact]
        public void asdadd()
        {
            //var boolean = false;

            //var command = new SubmitNewPersonCommand(new CreatePersonViewModel(
            //    _repository.Object,
            //    _eventAggregator.Object,
            //    _iUpload.Object));

            //command.CanExecuteChanged += Command_CanExecuteChanged;

            //command.RaiseCanExecuteChanged(this, EventArgs.Empty);
            //command.CanExecute(null);
            //command.Execute(null);
            //Console.WriteLine("Hello");
        }

        private void Command_CanExecuteChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Hello");
        }

        [Fact]
        public async void asdad()
        {
            var boolean = false;

            var command = new AsyncCommand(
                new Func<System.Threading.Tasks.Task>(async () =>
                {
                    Console.WriteLine("Executed");
                }),
                new Func<bool>(() =>
                {
                    return boolean;
                }));
            command.RaiseCanExecuteChanged(this, EventArgs.Empty);
            await command.ExecuteAsync();
        }

        //[Fact]
        //public void CanExecuteWithFirstNameNull()
        //{
        //    var vm = new CreatePersonViewModel(_repository.Object);

        //    vm.LastName = "asd";
        //    vm.DateOfBirth = DateTime.Parse("2010-10-10");

        //    Assert.False(vm.CanExecuteSubmit());
        //}

        //[Fact]
        //public void CanExecuteWithLastNameNull()
        //{
        //    var vm = new CreatePersonViewModel(_repository.Object);

        //    vm.FirstName = "asd";
        //    vm.DateOfBirth = DateTime.Parse("2010-10-10");

        //    Assert.False(vm.CanExecuteSubmit());
        //}

        //[Fact]
        //public void CanExecuteWithDateOfBirthNull()
        //{
        //    var vm = new CreatePersonViewModel(_repository.Object);

        //    vm.LastName = "asd";
        //    vm.FirstName = "asd";
        //    vm.DateOfBirth = DateTime.MinValue;
        //    Assert.False(vm.CanExecuteSubmit());
        //}

        //[Fact]
        //public void CanExecuteReturnTrue()
        //{
        //    var vm = new CreatePersonViewModel(_repository.Object);

        //    vm.LastName = "asd";
        //    vm.FirstName = "asd";
        //    vm.DateOfBirth = DateTime.Parse("2010-10-10");

        //    Assert.True(vm.CanExecuteSubmit());
        //}

    }
}
