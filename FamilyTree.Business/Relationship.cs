using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FamilyTree.Business
{
    public class Relationship : BusinessBase
    {
        private Person _personFrom;
        public Person PersonFrom
        {
            get { return _personFrom; }
            set { SetProperty(ref _personFrom, value); }
        }

        private Person _personTo;
        public Person PersonTo
        {
            get { return _personTo; }
            set { SetProperty(ref _personTo, value); }
        }

        private string _relationType;
        public string RelationType
        {
            get { return _relationType; }
            set { SetProperty(ref _relationType, value); }
        }

        private DateTime _from;
        public DateTime From
        {
            get { return _from; }
            set { SetProperty(ref _from, value); }
        }

        private DateTime _to;
        public DateTime To
        {
            get { return _to; }
            set { SetProperty(ref _to, value); }
        }

        public Relationship()
        {
        }
    }
}
