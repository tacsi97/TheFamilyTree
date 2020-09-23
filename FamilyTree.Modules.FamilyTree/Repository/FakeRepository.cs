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
        }

        public async Task CreateAsync(string uri, string content)
        {
            await Task.Run(() => Trees.Add(JsonConvert.DeserializeObject<Business.FamilyTree>(content)));
        }

        public async Task DeleteAsync(string uri, int id)
        {
            await Task.Run(() => Trees.RemoveAt(id));
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
                        tree.Name = treeFrom.Name;
                        tree.People = treeFrom.People;
                    }
                }
            });
        }
    }
}
