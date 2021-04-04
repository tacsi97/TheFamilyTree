using FamilyTree.Business;
using FamilyTree.Core;
using FamilyTree.Services.Repository;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.Modules.Person.Repository
{
    public class LocalGraphRepository : LocalRepositoryBase<Business.Person>
    {
        public LocalGraphRepository(string uri, Token token) : base(uri, token)
        {
        }

        public override async Task<Business.Person> CreateAsync(Business.Person content)
        {
            var client = new GraphClient(
                new Uri(DatabaseInfo.Uri),
                DatabaseInfo.UserName,
                DatabaseInfo.Password);

            await client.ConnectAsync();

            var results = await client.Cypher
                .Create("(person:Person)")
                .Set("person.FirstName = 'asd'")
                .With("person")
                .Match("(person)")
                .Return<Business.Person>("person")
                .ResultsAsync;

            var person = results.First();

            return person;
        }

        public override async Task DeleteAsync(string id)
        {
            var client = new GraphClient(
                new Uri(DatabaseInfo.Uri),
                DatabaseInfo.UserName,
                DatabaseInfo.Password);

            await client.ConnectAsync();

            await client.Cypher
                .Match("(person:Person)")
                .Where($"person.ID = '{ id }'")
                .DetachDelete("person")
                .ExecuteWithoutResultsAsync();
        }

        public override async Task<IEnumerable<Business.Person>> GetAllAsync()
        {
            var client = new GraphClient(
                new Uri(DatabaseInfo.Uri),
                DatabaseInfo.UserName,
                DatabaseInfo.Password);

            await client.ConnectAsync();

            return await client.Cypher
                .Match("(person:Person)")
                .Return<Business.Person>("person")
                .ResultsAsync;
        }

        public override async Task<Business.Person> GetAsync(string id)
        {
            var client = new GraphClient(
                new Uri(DatabaseInfo.Uri),
                DatabaseInfo.UserName,
                DatabaseInfo.Password);

            await client.ConnectAsync();

            var result = await client.Cypher
                .Match("(person:Person)")
                .Where<Business.Person>(p => p.ID == id)
                .Return<Business.Person>("person")
                .ResultsAsync;

            return result.FirstOrDefault();
        }

        public override async Task ModifyAsync(Business.Person content)
        {
            var client = new GraphClient(
                new Uri(DatabaseInfo.Uri),
                DatabaseInfo.UserName,
                DatabaseInfo.Password);

            await client.ConnectAsync();

            var result = await client.Cypher
                .Match("(person:Person)")
                .Where($"person.ID = '{ content.ID }'")
                .Set($"person.FirstName = '{ content.FirstName }'")
                .Set($"person.LastName = '{ content.LastName }'")
                .Set($"person.DateOfBirth = datetime('{ content.DateOfBirth:yyyy-MM-dd}')")
                .Set($"person.DateOfDeath = datetime('{ content.DateOfDeath:yyyy-MM-dd}')")
                .Set($"person.Gender = '{ content.Gender }'")
                .Set($"person.ImagePath = '{ content.ImagePath }'")
                .Return<Business.Person>("person")
                .ResultsAsync;
        }
    }
}
