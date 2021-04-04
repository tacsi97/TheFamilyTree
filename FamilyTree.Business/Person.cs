using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FamilyTree.Business
{
    public class Person : BusinessBase
    {
        private string _firstName;
        [JsonProperty("FirstName")]
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _lastName;
        [JsonProperty("LastName")]
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        private DateTime _dateOfBirth;
        [JsonProperty("DateOfBirth")]
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { SetProperty(ref _dateOfBirth, value); }
        }

        private DateTime _dateOfDeath;
        [JsonProperty("DateOfDeath")]
        public DateTime DateOfDeath
        {
            get { return _dateOfDeath; }
            set { SetProperty(ref _dateOfDeath, value); }
        }

        private Node _node;
        [JsonIgnore]
        public Node Node
        {
            get { return _node; }
            set { SetProperty(ref _node, value); }
        }

        private GenderType _gender;
        [JsonProperty("Gender")]
        public GenderType Gender
        {
            get { return _gender; }
            set { SetProperty(ref _gender, value); }
        }

        private FamilyTree _familyTree;
        [JsonIgnore]
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

        private Person _rightSibling;
        [JsonIgnore]
        public Person RightSibling
        {
            get { return _rightSibling; }
            set { SetProperty(ref _rightSibling, value); }
        }

        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set { SetProperty(ref _imagePath, value); }
        }

        private Person _partner;
        [JsonIgnore]
        public Person Partner
        {
            get { return _partner; }
            set { SetProperty(ref _partner, value); }
        }

        private Person _leftmostChild;
        [JsonIgnore]
        public Person LeftmostChild
        {
            get { return _leftmostChild; }
            set { SetProperty(ref _leftmostChild, value); }
        }

        private string _motherID;
        public string MotherID
        {
            get { return _motherID; }
            set { SetProperty(ref _motherID, value); }
        }

        private string _fatherID;
        public string FatherID
        {
            get { return _fatherID; }
            set { SetProperty(ref _fatherID, value); }
        }

        private string _partnerID;
        public string PartnerID
        {
            get { return _partnerID; }
            set { SetProperty(ref _partnerID, value); }
        }

        [JsonIgnore]
        public ICollection<Person> Children { get; set; }

        public Person()
        {
            Children = new List<Person>();
            ImagePath = "images/default-avatar.png";
        }
    }
}
