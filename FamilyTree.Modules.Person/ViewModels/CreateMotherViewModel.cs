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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FamilyTree.Modules.Person.ViewModels
{
    public class CreateMotherViewModel : BindableBase, INavigationAware
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
            if (op.ShowDialog() == true)
                NewPerson.Image = new BitmapImage(new Uri(op.FileName));
        }

        #endregion

        #region Properties
        private Business.Person _selectedPerson;
        public Business.Person SelectedPerson
        {
            get { return _selectedPerson; }
            set { SetProperty(ref _selectedPerson, value); }
        }

        private Business.Person _newPerson = new Business.Person();
        public Business.Person NewPerson
        {
            get { return _newPerson; }
            set { SetProperty(ref _newPerson, value); }
        }

        public string Title => "Személy létrehozása";

        #endregion

        public CreateMotherViewModel(IAsyncRepository<Business.Person> repository, IRegionManager regionManager)
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
                await _repository.CreateMother(SelectedPerson, NewPerson);

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
            SelectedPerson = navigationContext.Parameters.GetValue<Business.Person>(NavParamNames.Person);
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
