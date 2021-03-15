using FamilyTree.Core.Extensions;
using FamilyTree.Services.Repository;
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
    public class FakePersonRepository : LocalRepositoryBase<Business.Person>
    {
        public ObservableCollection<Business.Person> People = new ObservableCollection<Business.Person>();

        public int id = 0;

        public FakePersonRepository(string uri, Business.Token token)
            : base(uri, token)
        {
            People.Add(new Business.Person() { ID = 1, FirstName = "Anya" });
            People.Add(new Business.Person() { ID = 2, FirstName = "Apa" });
            People.Add(new Business.Person() { ID = 3, FirstName = "Gyerek1" });
            People.Add(new Business.Person() { ID = 4, FirstName = "Gyerek2" });
            People.Add(new Business.Person() { ID = 5, FirstName = "Gyerek3" });
            People.Add(new Business.Person() { ID = 6, FirstName = "Gyerek4" });
            People.Add(new Business.Person() { ID = 7, FirstName = "Gyerek1 Pár" });
            People.Add(new Business.Person() { ID = 8, FirstName = "Gyerek11" });
            People.Add(new Business.Person() { ID = 9, FirstName = "Gyerek12" });
            People.Add(new Business.Person() { ID = 10, FirstName = "Gyerek11 Pár" });
            // 8,9 anyja
            AddMother(People.First(person => person.ID == 8), People.First(person => person.ID == 3));
            AddMother(People.First(person => person.ID == 9), People.First(person => person.ID == 3));
            // 8,9 apja
            AddFather(People.First(person => person.ID == 8), People.First(person => person.ID == 7));
            AddFather(People.First(person => person.ID == 9), People.First(person => person.ID == 7));
            // 3,4,5,6 anyja
            AddMother(People.First(person => person.ID == 3), People.First(person => person.ID == 1));
            AddMother(People.First(person => person.ID == 4), People.First(person => person.ID == 1));
            AddMother(People.First(person => person.ID == 5), People.First(person => person.ID == 1));
            AddMother(People.First(person => person.ID == 6), People.First(person => person.ID == 1));
            // 3,4,5,6 apja
            AddFather(People.First(person => person.ID == 3), People.First(person => person.ID == 2));
            AddFather(People.First(person => person.ID == 4), People.First(person => person.ID == 2));
            AddFather(People.First(person => person.ID == 5), People.First(person => person.ID == 2));
            AddFather(People.First(person => person.ID == 6), People.First(person => person.ID == 2));
            // anya és apa
            AddPair(People.First(person => person.ID == 1), People.First(person => person.ID == 2));
            // Gyerek1 és Gyerek1 Pár
            AddPair(People.First(person => person.ID == 3), People.First(person => person.ID == 7));
            // Gyerek11 és Gyerek11 Pár
            AddPair(People.First(person => person.ID == 8), People.First(person => person.ID == 10));

        }

        public override async Task<Business.Person> CreateAsync(Business.Person content)
        {
            return await Task.Run(() =>
            {
                content.ID = id;
                id++;
                People.Add(content);
                return content;
            });
        }

        public void AddMother(Business.Person child, Business.Person mother)
        {
            child.Mother = mother;
            mother.Children.Add(child);
        }

        public void AddFather(Business.Person child, Business.Person father)
        {
            child.Father = father;
            father.Children.Add(child);
        }

        public void AddPair(Business.Person one, Business.Person two)
        {
            one.Partner = two;
            two.Partner = one;
        }

        public override async Task DeleteAsync(int id)
        {
            await Task.Run(() =>
            {
                foreach (var person in People)
                {
                    if (person.ID == id)
                    {
                        People.Remove(person);
                        break;
                    }
                }
            });
        }

        public override async Task<IEnumerable<Business.Person>> GetAllAsync()
        {
            var result = await Task.Run<IEnumerable<Business.Person>>(() => People);

            return result;
        }

        public override async Task<Business.Person> GetAsync(int id)
        {
            var result = await Task.Run(() => People.ElementAt(id));

            return result;
        }

        public override async Task ModifyAsync(Business.Person content)
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
