﻿using FamilyTree.Core.Extensions;
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
    public class FakePersonRepository : IAsyncRepository<Business.Person>
    {
        public ObservableCollection<Business.Person> People = new ObservableCollection<Business.Person>();

        public int id = 0;

        public async Task CreateAsync(string uri, string content)
        {
            await Task.Run(() =>
            {
                var person = JsonConvert.DeserializeObject<Business.Person>(content);
                person.ID = id;
                id++;
                People.Add(person);
                MessageBox.Show("Hi");
            });
        }

        public async Task DeleteAsync(string uri, int id)
        {
            await Task.Run(() =>
            {
                People.RemoveAt(id);
            });
        }

        public async Task<IEnumerable<Business.Person>> GetAllAsync(string uri)
        {
            var result = await Task.Run<IEnumerable<Business.Person>>(() => People);

            return result;
        }

        public async Task<Business.Person> GetAsync(string uri, int id)
        {
            var result = await Task.Run(() => People.ElementAt(id));

            return result;
        }

        public async Task ModifyAsync(string uri, string content)
        {
            await Task.Run(() =>
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