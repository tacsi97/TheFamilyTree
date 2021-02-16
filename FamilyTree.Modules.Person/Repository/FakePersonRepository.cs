using FamilyTree.Core.Extensions;
using FamilyTree.Services.Repository.Interfaces;
using Newtonsoft.Json;
using Prism.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FamilyTree.Modules.Person.Repository
{
    public class FakePersonRepository : IAsyncRemoteRepository<Business.Person>
    {
        public ObservableCollection<Business.Person> People = new ObservableCollection<Business.Person>();
        public Business.Token Token { get; set; }
        public string Uri { get; set; }

        public int id = 0;

        public FakePersonRepository()
        {
            People.Add(
                new Business.Person()
                {
                    ID = 0,
                    FirstName = "László",
                    LastName = "Tóth",
                    DateOfBirth = new DateTime(1997, 12, 1)
                });

            People.Add(
                new Business.Person()
                {
                    ID = 1,
                    FirstName = "Berci",
                    LastName = "Kutya",
                    DateOfBirth = new DateTime(2015, 4, 3)
                });

            People.Add(
                new Business.Person()
                {
                    ID = 2,
                    FirstName = "Kutya",
                    LastName = "Liza",
                    DateOfBirth = new DateTime(2016, 6, 11)
                });

            // Mivel már megadtunk 3 személyt
            id = 3;
        }

        public async Task CreateAsync(Business.Person content)
        {
            await Task.Run(() =>
            {
                content.ID = id;
                id++;
                People.Add(content);
            });
        }

        public async Task DeleteAsync(int id)
        {
            await Task.Run(() =>
            {
                People.RemoveAt(id);
            });
        }

        public async Task<IEnumerable<Business.Person>> GetAllAsync()
        {
            var result = await Task.Run<IEnumerable<Business.Person>>(() => People);

            return result;
        }

        public async Task<Business.Person> GetAsync(int id)
        {
            var result = await Task.Run(() => People.ElementAt(id));

            return result;
        }

        public async Task ModifyAsync(Business.Person content)
        {
            await Task.Run(() =>
            {
                var original = People.First(person => person.ID.Equals(content.ID));
                original.FirstName = content.FirstName;
                original.LastName = content.LastName;
                original.DateOfBirth = content.DateOfBirth;
                original.DateOfDeath = content.DateOfDeath;
                original.Gender = content.Gender;
            });
        }
    }
}
