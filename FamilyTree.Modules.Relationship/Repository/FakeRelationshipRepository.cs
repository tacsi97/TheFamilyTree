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
    public class FakeRelationshipRepository : LocalRepositoryBase<Business.Relationship>
    {
        public ObservableCollection<Business.Relationship> Relationships { get; set; }

        public FakeRelationshipRepository(string uri, Token token) : base(uri, token)
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

        public override async Task<Business.Relationship> CreateAsync(Business.Relationship content)
        {
            return await Task.Run(() =>
            {
                Relationships.Add(content);
                return content;
            });
        }

        public override async Task DeleteAsync(int id)
        {
            await Task.Run(() =>
            {
                var result = Relationships.ToList().Find((relationship) => relationship.ID == id);

                Relationships.Remove(result);
            });
        }

        public override async Task<IEnumerable<Business.Relationship>> GetAllAsync()
        {
            return await Task.Run(() => Relationships);
        }

        public override async Task<Business.Relationship> GetAsync(int id)
        {
            return await Task.Run(() =>
            {
                return Relationships.ToList().Find((relationship) => relationship.ID == id);
            });
        }

        public override async Task ModifyAsync(Business.Relationship content)
        {
            await Task.Run(() =>
            {
                var result = Relationships.ToList().Find((relationship) => relationship.ID == content.ID);

                result.PersonFrom = content.PersonFrom;
                result.PersonTo = content.PersonTo;
                result.From = content.From;
                result.To = content.To;
            });
        }
    }
}
