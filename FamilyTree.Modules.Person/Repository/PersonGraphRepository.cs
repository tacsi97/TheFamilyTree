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
    public class PersonGraphRepository : LocalRepositoryBase<Business.Person>
    {
        private readonly GraphClient _graphClient;

        // TODO: GraphRepositoryBase class, mivel sok az azonosság a függvényeknél
        // Write függvények csak módosítani nem adnak vissza értéket
        // Read függvények csak értéket adnak vissza, nem módosítanak
        public PersonGraphRepository(string uri, Business.Token token) : base(uri, token)
        {
            _graphClient = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password);
            _graphClient.ConnectAsync().FireAndForgetAsync();
        }


        public override async Task<Business.Person> CreateAsync(Business.Person template)
        {
            // TODO: kép kezelés (elérési útvonal)
            await _graphClient.Cypher
                .Match("(ft:FamilyTree)")
                .Where($"ft.ID = { template.FamilyTree.ID }")
                .Create("(p:Person)-[:IS_INCLUDED]->(ft)")
                .Set($"p.ID = { template.ID }")
                .Set($"p.FirstName = '{ template.FirstName }'")
                .Set($"p.LastName = '{ template.LastName }'")
                .Set($"p.DateOfBirth = date('{ template.DateOfBirth:yyyy-MM-dd}')")
                .Set($"p.DateOfDeath = date('{ template.DateOfDeath:yyyy-MM-dd}')")
                .Set($"p.Gender = '{ template.Gender }'")
                .ExecuteWithoutResultsAsync();

            // TODO: lekérdezni az előzőleg beszúrt emberkét
            return new Business.Person();
        }

        public override async Task DeleteAsync(string id)
        {
            // TODO: kép kezelés (elérési útvonal)
            await _graphClient.Cypher
                .Match("(p:Person)")
                .Where($"p.ID = { id }")
                .DetachDelete("p")
                .ExecuteWithoutResultsAsync();
        }

        public override async Task<IEnumerable<Business.Person>> GetAllAsync()
        {
            // TODO: kép kezelés (elérési útvonal)
            return await _graphClient.Cypher
                .Match("(p:Person)")
                .Return<Business.Person>("p")
                .ResultsAsync;
        }

        public override async Task<Business.Person> GetAsync(string id)
        {
            // TODO: kép kezelés (elérési útvonal)
            var result = await _graphClient.Cypher
                .Match("(p:Person)")
                .Where($"p.ID = { id }")
                .Return<Business.Person>("p")
                .ResultsAsync;

            return result.FirstOrDefault();
        }

        public override async Task ModifyAsync(Business.Person template)
        {
            // TODO: kép kezelés (elérési útvonal)
            await _graphClient.Cypher
                .Match("(p:Person)")
                .Where($"p.ID = { template.ID }")
                .Set($"p.FirstName = '{ template.FirstName }'")
                .Set($"p.LastName = '{ template.LastName }'")
                .Set($"p.DateOfBirth = date('{ template.DateOfBirth:yyyy-MM-dd}')")
                .Set($"p.DateOfDeath = date('{ template.DateOfDeath:yyyy-MM-dd}')")
                .Set($"p.Gender = '{ template.Gender }'")

                .ExecuteWithoutResultsAsync();
        }
    }
}
