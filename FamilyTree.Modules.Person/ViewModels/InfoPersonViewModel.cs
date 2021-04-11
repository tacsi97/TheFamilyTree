using FamilyTree.Business;
using FamilyTree.Core;
using FamilyTree.Core.Commands;
using FamilyTree.Modules.Person.Core;
using FamilyTree.Services.Repository.Interfaces;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FamilyTree.Modules.Person.ViewModels
{
    public class InfoPersonViewModel : BindableBase, INavigationAware
    {
        #region Fields

        private readonly IAsyncRepository<Business.Person> _repository;
        private readonly IRegionManager _regionManager;

        #endregion

        #region Properties

        private string _id;
        public string ID
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                SetProperty(ref _firstName, value);
                AsyncCommand.RaiseCanExecuteChanged(this, EventArgs.Empty);
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                SetProperty(ref _lastName, value);
                AsyncCommand.RaiseCanExecuteChanged(this, EventArgs.Empty);
            }
        }

        private DateTime? _dateOfBirth;
        public DateTime? DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                SetProperty(ref _dateOfBirth, value);
                AsyncCommand.RaiseCanExecuteChanged(this, EventArgs.Empty);
            }
        }

        private DateTime? _dateOfDeath;
        public DateTime? DateOfDeath
        {
            get { return _dateOfDeath; }
            set
            {
                SetProperty(ref _dateOfDeath, value);
                AsyncCommand.RaiseCanExecuteChanged(this, EventArgs.Empty);
            }
        }

        private GenderType _gender;
        public GenderType Gender
        {
            get { return _gender; }
            set
            {
                SetProperty(ref _gender, value);
                AsyncCommand.RaiseCanExecuteChanged(this, EventArgs.Empty);
            }
        }

        private string _imagePath = "";
        public string ImagePath
        {
            get { return _imagePath; }
            set { SetProperty(ref _imagePath, value); }
        }

        private bool _isDead;
        public bool IsDead
        {
            get { return _isDead; }
            set { SetProperty(ref _isDead, value); }
        }

        public string Title => "Személy létrehozása";

        public ObservableCollection<Business.Person> People { get; set; }

        #endregion

        #region Commands

        private AsyncCommand _asyncCommand;
        public AsyncCommand AsyncCommand
        {
            get { return _asyncCommand; }
            set { SetProperty(ref _asyncCommand, value); }
        }

        #endregion

        public InfoPersonViewModel(IAsyncRepository<Business.Person> repository, IRegionManager regionManager)
        {
            _repository = repository;
            _regionManager = regionManager;

            AsyncCommand = new AsyncCommand(ExecuteSubmit, CanExecuteSubmit);
        }

        public async Task ExecuteSubmit()
        {
            try
            {
                var person = new Business.Person()
                {
                    ID = ID,
                    FirstName = FirstName,
                    LastName = LastName,
                    DateOfBirth = DateOfBirth,
                    DateOfDeath = DateOfDeath,
                    Gender = Gender
                };

                await _repository.ModifyAsync(person);

                var navParams = new NavigationParameters();
                navParams.Add(NavParamNames.Person, person);

                _regionManager.RequestNavigate(RegionNames.ContentRegion, "ListPersonView", navParams);
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
            var person = navigationContext.Parameters.GetValue<Business.Person>(NavParamNames.Person);

            ID = person.ID;
            FirstName = person.FirstName;
            LastName = person.LastName;
            DateOfBirth = person.DateOfBirth;
            DateOfDeath = person.DateOfDeath;
            Gender = person.Gender;
            ImagePath = person.ImagePath;
            IsDead = person.IsDead;
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
