using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FamilyTree.Business
{
    public class FamilyTree : BusinessBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public ObservableCollection<Person> People { get; set; }
    }
}
