using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FamilyTree.Business
{
    public class Relationship : BusinessBase
    {
        public ObservableCollection<Business.Person> Members { get; set; }

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
    }
}
