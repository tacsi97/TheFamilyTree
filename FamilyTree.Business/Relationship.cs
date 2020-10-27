using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTree.Business
{
    public class Relationship : BusinessBase
    {
        private Person _partner;
        [JsonIgnore]
        public Person Partner
        {
            get { return _partner; }
            set { SetProperty(ref _partner, value); }
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
    }
}
