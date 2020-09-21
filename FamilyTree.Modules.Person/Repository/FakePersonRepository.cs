using FamilyTree.Services.Repository.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Modules.Person.Repository
{
    public class FakePersonRepository : IAsyncRepository<Business.Person>
    {
        public static List<Business.Person> People = new List<Business.Person>();

        public static int id = 0;

        public async Task CreateAsync(string uri, string content)
        {
            await new Task(() =>
            {
                var person = JsonConvert.DeserializeObject<Business.Person>(content);
                person.ID = id;
                id++;
                People.Add(person);
            });
        }

        public async Task DeleteAsync(string uri, int id)
        {
            await new Task(() =>
            {
                People.RemoveAt(id);
            });
        }

        public async Task<IEnumerable<Business.Person>> GetAllAsync(string uri)
        {
            var result = await new Task<IEnumerable<Business.Person>>(() => People);

            return result;
        }

        public async Task<Business.Person> GetAsync(string uri, int id)
        {
            var result = await new Task<Business.Person>(() => People.ElementAt(id));

            return result;
        }

        public async Task ModifyAsync(string uri, string content)
        {
            await new Task(() =>
            {
                var resultPerson = JsonConvert.DeserializeObject<Business.Person>(content);
                foreach (var person in People)
                {
                    if (person.ID == resultPerson.ID)
                    {
                        person.FirstName = resultPerson.FirstName;
                        person.LastName = resultPerson.LastName;
                        person.DateOfBirth = resultPerson.DateOfBirth;
                        person.DateOfDeath = resultPerson.DateOfDeath;
                        person.Gender = resultPerson.Gender;

                        break;
                    }
                }
            });
        }
    }
}
