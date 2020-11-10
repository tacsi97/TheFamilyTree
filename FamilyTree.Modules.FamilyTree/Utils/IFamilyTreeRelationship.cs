using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTree.Modules.FamilyTree.Utils
{
    interface IFamilyTreeRelationship
    {
        public int ID { get; set; }
        public IFamilyTreeMember Partner { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        // TODO: Adapterekkel kell kivenni minden olyan hivatkozást, ami a másik modulra vonatkozik
        // TODO: Adaptereket megnézni a PRISM Outlook-ban
    }
}
