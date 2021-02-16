using FamilyTree.Business;
using FamilyTree.Services.Repository.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Modules.FamilyTree.Repository
{
    public class FakeRepository : IAsyncRemoteRepository<Business.FamilyTree>
    {
        public ObservableCollection<Business.FamilyTree> Trees { get; set; }
        public Token Token { get; set; }
        public string Uri { get; set; }

        public FakeRepository()
        {
            Trees = new ObservableCollection<Business.FamilyTree>();
            Trees.Add(new Business.FamilyTree() { ID = 0, Name = "Tóth Család", People = new ObservableCollection<Person>() });
            Trees.Add(new Business.FamilyTree() { ID = 1, Name = "Fodor Család", People = new ObservableCollection<Person>() });
            Trees.Add(new Business.FamilyTree() { ID = 2, Name = "Láng Család", People = new ObservableCollection<Person>() });
        }

        public async Task CreateAsync(Business.FamilyTree content)
        {
            await Task.Run(() => Trees.Add(content));
        }

        public async Task DeleteAsync(int id)
        {
            await Task.Run(() =>
            {
                foreach (var tree in Trees)
                {
                    if (tree.ID == id)
                    {
                        Trees.Remove(tree);
                        break;
                    }
                }
            });
        }

        public async Task<IEnumerable<Business.FamilyTree>> GetAllAsync()
        {
            return await Task.Run(() => Trees);
        }

        public async Task<Business.FamilyTree> GetAsync(int id)
        {
            return await Task.Run(() => Trees.ElementAt(id));
        }

        public async Task ModifyAsync(Business.FamilyTree content)
        {
            await Task.Run(() =>
            {
                var original = Trees.First(tree => tree.ID.Equals(content.ID));

                original.ID = content.ID;
                original.Name = content.Name;
                original.People = content.People;
            });
        }
    }
}
