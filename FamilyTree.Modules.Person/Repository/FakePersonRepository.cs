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
            People.Add(
                new Business.Person()
                {
                    ID = 0,
                    FirstName = "László",
                    LastName = "Tóth",
                    DateOfBirth = new DateTime(1997, 12, 1),
                    FamilyTree = new Business.FamilyTree()
                    {
                        ID = 0
                    },
                    Mother = new Business.Person()
                    {
                        ID = 3,
                        FirstName = "Erzsébet",
                        LastName = "Vidos",
                        Mother = new Business.Person()
                        {
                            ID = 4,
                            FirstName = "Anna",
                            LastName = "Horváth"
                        }
                    },
                    Father = new Business.Person()
                    {
                        ID = 5,
                        FirstName = "László",
                        LastName = "Tóth",
                        Mother = new Business.Person()
                        {
                            ID = 6,
                            FirstName = "Mária",
                            LastName = "Nagy"
                        },
                        Father = new Business.Person()
                        {
                            ID = 7,
                            FirstName = "Győző",
                            LastName = "Tóth"
                        }
                    }
                });

            People.Add(
                new Business.Person()
                {
                    ID = 1,
                    FirstName = "Berci",
                    LastName = "Kutya",
                    DateOfBirth = new DateTime(2015, 4, 3),
                    FamilyTree = new Business.FamilyTree()
                    {
                        ID = 0
                    }
                });

            People.Add(
                new Business.Person()
                {
                    ID = 2,
                    FirstName = "Kutya",
                    LastName = "Liza",
                    DateOfBirth = new DateTime(2016, 6, 11),
                    FamilyTree = new Business.FamilyTree()
                    {
                        ID = 0
                    }
                });

            // Mivel már megadtunk 3 személyt
            id = 3;
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

        public override async Task DeleteAsync(int id)
        {
            await Task.Run(() =>
            {
                foreach(var person in People)
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
