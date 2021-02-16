﻿using FamilyTree.Business;
using FamilyTree.Core;
using FamilyTree.Core.Commands;
using FamilyTree.Core.PubSubEvents;
using FamilyTree.Modules.Person.Commands;
using FamilyTree.Services.Repository.Interfaces;
using Microsoft.Win32;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FamilyTree.Modules.Person.ViewModels
{
    public class ModifyPersonViewModel : BindableBase, IDialogAware
    {
        private readonly IAsyncRemoteRepository<Business.Person> _repository;

        private AsyncCommand _asyncCommand;
        public AsyncCommand AsyncCommand
        {
            get { return _asyncCommand; }
            set { SetProperty(ref _asyncCommand, value); }
        }

        #region Properties
        private int _id;
        public int ID
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

        private DateTime _dateOfBirth;
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                SetProperty(ref _dateOfBirth, value);
                AsyncCommand.RaiseCanExecuteChanged(this, EventArgs.Empty);
            }
        }

        private DateTime _dateOfDeath;
        public DateTime DateOfDeath
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

        private BitmapImage _image;
        public BitmapImage Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }

        public string Title => "Személy létrehozása";

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
                Image = new BitmapImage(new Uri(op.FileName));
        }

        public ObservableCollection<Business.Person> People { get; set; }

        #endregion

        public event Action<IDialogResult> RequestClose;

        public ModifyPersonViewModel(IAsyncRemoteRepository<Business.Person> repository)
        {
            _repository = repository;

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

                RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
            }
            catch (Exception e)
            {
                // TODO: exception handling
            }
        }

        /// <summary>
        /// This determines whether the command can be executed or not.
        /// If the <c>NewPersonDialogViewModel</c>'s FirstName or LastName attribute is null or empty, or the DateOfBirth is minvalue.
        /// </summary>
        /// <returns>True or false</returns>
        public bool CanExecuteSubmit()
        {
            // TODO: Create a generic validator class, what takes a parameter with the given type, and returns that the value is valid or not
            if (string.IsNullOrEmpty(FirstName)
                || string.IsNullOrEmpty(LastName)
                || DateOfBirth == DateTime.MinValue)
                return false;
            return true;
        }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            var person = parameters.GetValue<Business.Person>("SelectedPerson");

            ID = person.ID;
            FirstName = person.FirstName;
            LastName = person.LastName;
            DateOfBirth = person.DateOfBirth;
            DateOfDeath = person.DateOfDeath;
            Gender = person.Gender;
        }
    }
}
