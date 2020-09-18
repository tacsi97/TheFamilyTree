﻿using FamilyTree.Business;
using FamilyTree.Core;
using FamilyTree.Services.Interfaces;
using FamilyTree.Services.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Services
{
    public class PersonService : IPersonService
    {
        private HttpRepository<Person> _httpRepository;

        public PersonService(HttpRepository<Person> repository)
        {
            _httpRepository = repository;
        }

        public async Task<bool> AddPerson(Person person)
        {
            var response = await _httpRepository.Create(person);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeletePerson(int id)
        {
            var response = await _httpRepository.Delete(id);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<Person>> GetPeople()
        {
            var response = await _httpRepository.GetAll();

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var data = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<List<Person>>(data);
        }

        public async Task<Person> GetPerson(int id)
        {
            var response = await _httpRepository.Get(id);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var data = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Person>(data);
        }

        public async Task<bool> ModifyPerson(Person person)
        {
            var response = await _httpRepository.Modify(person);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }
    }
}
