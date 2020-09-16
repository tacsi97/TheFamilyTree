using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

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
    }
}
