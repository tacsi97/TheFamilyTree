using FamilyTree.Business;
using FamilyTree.Modules.Relationship.Core;
using FamilyTree.Services.Repository;
using FamilyTree.Services.Repository.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FamilyTree.Modules.Relationship.Repository
{
    public class FakeRelationshipRepository : IAsyncRepository<Business.Relationship>
    {
        public ObservableCollection<Business.Relationship> Relationships { get; set; }

        public FakeRelationshipRepository()
        {
            Relationships = new ObservableCollection<Business.Relationship>();

            Relationships.Add(new Business.Relationship()
            {
                RelationType = TypeNames.Partner,
                From = DateTime.Now,
                To = DateTime.Now,
                PersonFrom = new Person()
                {
                    ID = 0,
                    FirstName = "PFhzf",
                    LastName = "Asd",
                    Gender = GenderType.Male
                },
                PersonTo = new Person()
                {
                    ID = 1,
                    FirstName = "FzhFP",
                    LastName = "Dsa",
                    Gender = GenderType.Female
                }
            });
        }

        public async Task CreateAsync(string uri, string content)
        {
            await Task.Run(() =>
            {
                Relationships.Add(JsonConvert.DeserializeObject<Business.Relationship>(content));
            });
        }

        public async Task DeleteAsync(string uri, int id)
        {
            await Task.Run(() =>
            {
                var result = Relationships.ToList().Find((relationship) => relationship.ID == id);

                Relationships.Remove(result);
            });
        }

        public async Task<IEnumerable<Business.Relationship>> GetAllAsync(string uri)
        {
            return await Task.Run(() => Relationships);
        }

        public async Task<Business.Relationship> GetAsync(string uri, int id)
        {
            return await Task.Run(() =>
            {
                return Relationships.ToList().Find((relationship) => relationship.ID == id);
            });
        }

        public async Task ModifyAsync(string uri, string content)
        {
            var contentRelationship = JsonConvert.DeserializeObject<Business.Relationship>(content);

            await Task.Run(() =>
            {
                var result = Relationships.ToList().Find((relationship) => relationship.ID == contentRelationship.ID);

                result.PersonFrom = contentRelationship.PersonFrom;
                result.PersonTo = contentRelationship.PersonTo;
                result.From = contentRelationship.From;
                result.To = contentRelationship.To;
            });
        }
    }
}
