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
    public class CreateChildViewModel : BindableBase, INavigationAware
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

        private DelegateCommand _selectPictureCommand;
        public DelegateCommand SelectPictureCommand =>
            _selectPictureCommand ?? (_selectPictureCommand = new DelegateCommand(ExecuteSelectPictureCommand));

        void ExecuteSelectPictureCommand()
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

            NewPerson.ImagePath = Path.Combine("images", fileName);
        }

        #endregion

        #region Properties

        private Business.Person _mother;
        public Business.Person Mother
        {
            get { return _mother; }
            set { SetProperty(ref _mother, value); }
        }

        private Business.Person _father;
        public Business.Person Father
        {
            get { return _father; }
            set { SetProperty(ref _father, value); }
        }

        private Business.Person _newPerson = new Business.Person();
        public Business.Person NewPerson
        {
            get { return _newPerson; }
            set { SetProperty(ref _newPerson, value); }
        }

        public string Title => "Személy létrehozása";

        #endregion

        public CreateChildViewModel(IAsyncRepository<Business.Person> repository, IRegionManager regionManager)
        {
            _repository = repository;
            _regionManager = regionManager;

            NewPerson = new Business.Person();

            AsyncCommand = new AsyncCommand(Submit, CanExecuteSubmit);
        }

        public async Task Submit()
        {
            try
            {
                await _repository.CreateChild(Father, Mother, NewPerson);

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
            Father = navigationContext.Parameters.GetValue<Business.Person>(NavParamNames.Father);
            Mother = navigationContext.Parameters.GetValue<Business.Person>(NavParamNames.Mother);
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
