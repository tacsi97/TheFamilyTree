using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace FamilyTree.Business
{
    public class Person : BusinessBase
    {
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        private DateTime _dateOfBirth;
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { SetProperty(ref _dateOfBirth, value); }
        }

        private DateTime _dateOfDeath;
        public DateTime DateOfDeath
        {
            get { return _dateOfDeath; }
            set { SetProperty(ref _dateOfDeath, value); }
        }

        private GenderType _gender;
        public GenderType Gender
        {
            get { return _gender; }
            set { SetProperty(ref _gender, value); }
        }

        private FamilyTree _familyTree;
        public FamilyTree FamilyTree
        {
            get { return _familyTree; }
            set { SetProperty(ref _familyTree, value); }
        }

        private Person _father;
        [JsonIgnore]
        public Person Father
        {
            get { return _father; }
            set { SetProperty(ref _father, value); }
        }

        private Person _mother;
        [JsonIgnore]
        public Person Mother
        {
            get { return _mother; }
            set { SetProperty(ref _mother, value); }
        }

        private BitmapImage _image;
        public BitmapImage Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }

        [JsonIgnore]
        public ObservableCollection<Person> Children { get; set; }

        [JsonIgnore]
        public ObservableCollection<Relationship> Partners { get; set; }

        public Person()
        {
            Children = new ObservableCollection<Person>();
            Partners = new ObservableCollection<Relationship>();
        }
    }
}
