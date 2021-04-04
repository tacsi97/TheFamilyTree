using FamilyTree.Business;
using System;
using System.Collections.Generic;

namespace FamilyTree.Modules.FamilyTree.Utils
{
    public interface IFamilyTreeMember
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfDeath { get; set; }
        public GenderType Gender { get; set; }
        public IFamilyTreeMember Father { get; set; }
        public IFamilyTreeMember Mother { get; set; }
        public ICollection<IFamilyTreeMember> Children { get; set; }
        public ICollection<IFamilyTreeMember> Partners { get; set; }
    }
}
