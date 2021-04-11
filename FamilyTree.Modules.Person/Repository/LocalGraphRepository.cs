using FamilyTree.Business;
using FamilyTree.Core;
using FamilyTree.Core.Extensions;
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
        private readonly GraphClient _graphClient;

        public LocalGraphRepository(string uri, Token token) : base(uri, token)
        {
            _graphClient = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password);
            _graphClient.ConnectAsync().FireAndForgetAsync();
        }

        public override async Task<Business.Person> CreateAsync(Business.Person content)
        {
            var results = await _graphClient.Cypher
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
            await _graphClient.Cypher
                .Match("(person:Person)")
                .Where($"person.ID = '{ id }'")
                .DetachDelete("person")
                .ExecuteWithoutResultsAsync();
        }

        public override async Task<IEnumerable<Business.Person>> GetAllAsync()
        {
            return await _graphClient.Cypher
                .Match("(person:Person)")
                .Return<Business.Person>("person")
                .ResultsAsync;
        }

        public override async Task<Business.Person> GetAsync(string id)
        {
            var result = await _graphClient.Cypher
                .Match("(person:Person)")
                .Where<Business.Person>(p => p.ID == id)
                .Return<Business.Person>("person")
                .ResultsAsync;

            return result.FirstOrDefault();
        }

        public override async Task ModifyAsync(Business.Person content)
        {
            var result = await _graphClient.Cypher
                .Match("(person:Person)")
                .Where($"person.ID = '{ content.ID }'")
                .Set($"person.FirstName = '{ content.FirstName }'")
                .Set($"person.LastName = '{ content.LastName }'")
                .Set($"person.{ nameof(content.DateOfBirth) } = null")
                .Set($"person.{ nameof(content.DateOfDeath) } = null")
                .Set($"person.Gender = '{ content.Gender }'")
                .Set($"person.ImagePath = '{ content.ImagePath }'")
                .Set($"person.{ nameof(content.IsDead) } = { content.IsDead }")
                .Return<Business.Person>("person")
                .ResultsAsync;

            if (content.DateOfBirth != null)
                await _graphClient.Cypher
                    .Match("(content:Person)")
                    .Where($"content.{ nameof(content.ID) } = '{ result.FirstOrDefault().ID }'")
                    .Set($"content.{ nameof(content.DateOfBirth) } = date('{ content.DateOfBirth:yyyy-MM-dd}')")
                    .ExecuteWithoutResultsAsync();

            if (content.DateOfDeath != null && content.IsDead)
                await _graphClient.Cypher
                    .Match("(content:Person)")
                    .Where($"content.{ nameof(content.ID) } = '{ result.FirstOrDefault().ID }'")
                    .Set($"content.{ nameof(content.DateOfDeath) } = date('{ content.DateOfDeath:yyyy-MM-dd}')")
                    .ExecuteWithoutResultsAsync();
        }
    }
}
