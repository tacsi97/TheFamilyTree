using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FamilyTree.Business
{
    public class FamilyTree : BusinessBase
    {
        public ObservableCollection<Person> people { get; set; }
    }
}
