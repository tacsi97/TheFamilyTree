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
    public class FakeRepository : IAsyncRepository<Business.FamilyTree>
    {
        public ObservableCollection<Business.FamilyTree> Trees { get; set; }

        public FakeRepository()
        {
            Trees = new ObservableCollection<Business.FamilyTree>();
            Trees.Add(new Business.FamilyTree() { ID = 0, Name = "Tóth Család", People = new ObservableCollection<Person>() });
            Trees.Add(new Business.FamilyTree() { ID = 1, Name = "Fodor Család", People = new ObservableCollection<Person>() });
            Trees.Add(new Business.FamilyTree() { ID = 2, Name = "Láng Család", People = new ObservableCollection<Person>() });
        }

        public async Task CreateAsync(string uri, string content)
        {
            await Task.Run(() => Trees.Add(JsonConvert.DeserializeObject<Business.FamilyTree>(content)));
        }

        public async Task DeleteAsync(string uri, int id)
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

        public async Task<IEnumerable<Business.FamilyTree>> GetAllAsync(string uri)
        {
            return await Task.Run(() => Trees);
        }

        public async Task<Business.FamilyTree> GetAsync(string uri, int id)
        {
            return await Task.Run(() => Trees.ElementAt(id));
        }

        public async Task ModifyAsync(string uri, string content)
        {
            await Task.Run(() =>
            {
                var treeFrom = JsonConvert.DeserializeObject<Business.FamilyTree>(content);
                foreach (var tree in Trees)
                {
                    if(tree.ID == treeFrom.ID)
                    {
                        treeFrom.Name = tree.Name;
                        treeFrom.People = tree.People;
                        break;
                    }
                }
            });
        }
    }
}
