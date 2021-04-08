using FamilyTree.Core;
using FamilyTree.Core.Commands;
using FamilyTree.Modules.Person.Core;
using FamilyTree.Modules.Person.Extensions;
using FamilyTree.Services.Repository.Interfaces;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FamilyTree.Modules.Person.ViewModels
{
    public class CreateParentsViewModel : BindableBase, INavigationAware
    {
        private readonly IAsyncRepository<Business.Person> _repository;
        private readonly IRegionManager _regionManager;

        #region Commands

        private AsyncCommand _asyncCommand;
        public AsyncCommand AsyncCommand
        {
            get { return _asyncCommand; }
            set { SetProperty(ref _asyncCommand, value); }
        }

        private DelegateCommand _selectMotherPictureCommand;
        public DelegateCommand SelectMotherPictureCommand =>
            _selectMotherPictureCommand ?? (_selectMotherPictureCommand = new DelegateCommand(ExecuteSelectMotherPictureCommand));

        void ExecuteSelectMotherPictureCommand()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                        "Portable Network Graphic (*.png)|*.png";

            if (op.ShowDialog() == false)
                return;

            var fileName = Path.GetFileName(op.FileName);

            if (!File.Exists(Path.Combine(Environment.CurrentDirectory, "images", fileName)))
                File.Copy(
                    op.FileName,
                    Path.Combine(Environment.CurrentDirectory, "images", fileName));

            Mother.ImagePath = Path.Combine("images", fileName);
        }

        private DelegateCommand _selectFatherPictureCommand;
        public DelegateCommand SelectFatherPictureCommand =>
            _selectFatherPictureCommand ?? (_selectFatherPictureCommand = new DelegateCommand(ExecuteSelectFatherPictureCommand));

        void ExecuteSelectFatherPictureCommand()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                        "Portable Network Graphic (*.png)|*.png";

            if (op.ShowDialog() == false)
                return;

            var fileName = Path.GetFileName(op.FileName);

            if (!File.Exists(Path.Combine(Environment.CurrentDirectory, "images", fileName)))
                File.Copy(
                    op.FileName,
                    Path.Combine(Environment.CurrentDirectory, "images", fileName));

            Father.ImagePath = Path.Combine("images", fileName);
        }

        #endregion

        #region Properties

        private Business.Person _mother = new Business.Person();
        public Business.Person Mother
        {
            get { return _mother; }
            set { SetProperty(ref _mother, value); }
        }

        private Business.Person _father = new Business.Person();
        public Business.Person Father
        {
            get { return _father; }
            set { SetProperty(ref _father, value); }
        }

        private Business.Person _child;
        public Business.Person Child
        {
            get { return _child; }
            set { SetProperty(ref _child, value); }
        }

        public string Title => "Személy létrehozása";

        #endregion

        public CreateParentsViewModel(IAsyncRepository<Business.Person> repository, IRegionManager regionManager)
        {
            _repository = repository;
            _regionManager = regionManager;

            AsyncCommand = new AsyncCommand(Submit, CanExecuteSubmit);
        }

        public async Task Submit()
        {
            try
            {
                var father = await _repository.CreateFather(Child, Father);
                var mother = await _repository.CreateMother(Child, Mother);

                await _repository.MakePair(mother, father);

                _regionManager.RequestNavigate(RegionNames.ContentRegion, "ListPersonView");
            }
            catch (Exception e)
            {
                // TODO: exception handling
            }
        }

        public bool CanExecuteSubmit()
        {
            return true;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Child = navigationContext.Parameters.GetValue<Business.Person>(NavParamNames.Person);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            return;
        }
    }
}
