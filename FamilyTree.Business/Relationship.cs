using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTree.Business
{
    public class Relationship : BusinessBase
    {
        public Person Partner { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}
