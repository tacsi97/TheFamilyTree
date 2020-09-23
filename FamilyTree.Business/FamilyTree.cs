using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FamilyTree.Business
{
    public class FamilyTree : BusinessBase
    {
        public string Name { get; set; }

        public ObservableCollection<Person> People { get; set; }
    }
}
