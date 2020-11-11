using System;
using System.Collections.Generic;

namespace FamilyTree.Services.PersonConnector.Interfaces
{
    public interface IPersonConnector
    {
        public ICollection<Business.Person> ConnectPeople(IEnumerable<Business.Relationship> relationships);
    }
}
