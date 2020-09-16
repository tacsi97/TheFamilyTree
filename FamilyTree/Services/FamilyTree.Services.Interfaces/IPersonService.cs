using FamilyTree.Business;
using FamilyTree.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Services.Interfaces
{
    public interface IPersonService
    {
        public Task<Person> GetPerson(int id);

        public Task<IEnumerable<Person>> GetPeople();

        public Task<bool> AddPerson(Person person);

        public Task<bool> ModifyPerson(Person person);

        public Task<bool> DeletePerson(int id);
    }
}
