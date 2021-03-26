using FamilyTree.Core;
using FamilyTree.Services.Repository.Interfaces;
using Neo4j.Driver;
using Neo4jClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Modules.Person.Repository
{
    public class PersonGraphRepository : IAsyncRepository<Business.Person>
    {
        public Business.Token Token { get; set; }
        public string Uri { get; set; }

        // TODO: GraphRepositoryBase class, mivel sok az azonosság a függvényeknél
        // Write függvények csak módosítani nem adnak vissza értéket
        // Read függvények csak értéket adnak vissza, nem módosítanak
        public PersonGraphRepository(string uri, string user, string password)
        {
        }

        public async Task<Business.Person> CreateAsync(Business.Person template)
        {
            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                // TODO: kép kezelés (elérési útvonal)
                await gc.Cypher
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
        }

        public async Task DeleteAsync(string id)
        {
            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                // TODO: kép kezelés (elérési útvonal)
                await gc.Cypher
                    .Match("(p:Person)")
                    .Where($"p.ID = { id }")
                    .DetachDelete("p")
                    .ExecuteWithoutResultsAsync();
            }
        }

        public async Task<IEnumerable<Business.Person>> GetAllAsync()
        {
            var people = new List<Business.Person>();

            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                // TODO: kép kezelés (elérési útvonal)
                var result = await gc.Cypher
                    .Match("(p:Person)")
                    .Return<Business.Person>("p")
                    .ResultsAsync;

                foreach (var person in result)
                {
                    people.Add(person);
                }
            }

            return people;
        }

        public async Task<Business.Person> GetAsync(string id)
        {
            Business.Person person = null;

            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                // TODO: kép kezelés (elérési útvonal)
                var result = await gc.Cypher
                    .Match("(p:Person)")
                    .Where($"p.ID = { id }")
                    .Return<Business.Person>("p")
                    .ResultsAsync;

                foreach (var per in result)
                {
                    person = per;
                }
            }

            return person;
        }

        public async Task ModifyAsync(Business.Person template)
        {
            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                // TODO: kép kezelés (elérési útvonal)
                await gc.Cypher
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
}
