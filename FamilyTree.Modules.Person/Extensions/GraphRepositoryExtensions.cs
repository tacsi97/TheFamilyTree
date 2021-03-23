using FamilyTree.Business;
using FamilyTree.Core;
using FamilyTree.Services.Repository.Interfaces;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Modules.Person.Extensions
{
    public static class GraphRepositoryExtensions
    {
        public async static Task CreateMother(IAsyncRepository<Business.Person> asyncRepository, Business.Person child, Business.Person mother)
        {
            var client = new GraphClient(
                new Uri(DatabaseInfo.Uri),
                DatabaseInfo.UserName,
                DatabaseInfo.Password);

            await client.ConnectAsync();

            await client.Cypher
                .Match("(child:Person)")
                .Where<Business.Person>(p => p.ID == child.ID)
                .With("child")
                .Create("(child)<-[:MOTHER_OF]-(mother:Person)")
                .Set("mother.ID = apoc.create.uuid()")
                .Set($"mother.FirstName = '{ mother.FirstName }'")
                .Set($"mother.LastName = '{ mother.LastName }'")
                .Set($"mother.DateOfBirth = datetime('{ mother.DateOfBirth:yyyy-MM-dd}')")
                .Set($"mother.DateOfDeath = datetime('{ mother.DateOfDeath:yyyy-MM-dd}')")
                .Set($"mother.Gender = '{ mother.Gender }'")
                .Set($"mother.ImagePath = '{ mother.Image.BaseUri }'")
                .With("child, mother")
                .Create("(child)-[:CHILD_OF]->(mother)")
                .ExecuteWithoutResultsAsync();
        }

        public async static Task CreateFather(IAsyncRepository<Business.Person> asyncRepository, Business.Person child, Business.Person father)
        {
            var client = new GraphClient(
                new Uri(DatabaseInfo.Uri),
                DatabaseInfo.UserName,
                DatabaseInfo.Password);

            await client.ConnectAsync();

            await client.Cypher
                .Match("(child:Person)")
                .Where<Business.Person>(p => p.ID == child.ID)
                .With("child")
                .Create("(child)<-[:FATHER_OF]-(father:Person)")
                .Set("father.ID = apoc.create.uuid()")
                .Set($"father.FirstName = '{ father.FirstName }'")
                .Set($"father.LastName = '{ father.LastName }'")
                .Set($"father.DateOfBirth = datetime('{ father.DateOfBirth:yyyy-MM-dd}')")
                .Set($"father.DateOfDeath = datetime('{ father.DateOfDeath:yyyy-MM-dd}')")
                .Set($"father.Gender = '{ father.Gender }'")
                .Set($"father.ImagePath = '{ father.Image.BaseUri }'")
                .With("child, father")
                .Create("(child)-[:CHILD_OF]->(father)")
                .ExecuteWithoutResultsAsync();
        }

        public async static Task CreatePair(IAsyncRepository<Business.Person> asyncRepository, Business.Person selected, Business.Person pair)
        {
            var client = new GraphClient(
                new Uri(DatabaseInfo.Uri),
                DatabaseInfo.UserName,
                DatabaseInfo.Password);

            await client.ConnectAsync();

            await client.Cypher
                .Match("(selected:Person)")
                .Where<Business.Person>(p => p.ID == selected.ID)
                .With("selected")
                .Create("(selected)<-[:PARTNER_OF]-(pair:Person)")
                .Set("pair.ID = apoc.create.uuid()")
                .Set($"pair.FirstName = '{ pair.FirstName }'")
                .Set($"pair.LastName = '{ pair.LastName }'")
                .Set($"pair.DateOfBirth = datetime('{ pair.DateOfBirth:yyyy-MM-dd}')")
                .Set($"pair.DateOfDeath = datetime('{ pair.DateOfDeath:yyyy-MM-dd}')")
                .Set($"pair.Gender = '{ pair.Gender }'")
                .Set($"pair.ImagePath = '{ pair.Image.BaseUri }'")
                .With("selected, pair")
                .Create("(selected)-[:PARTNER_OF]->(pair)")
                .ExecuteWithoutResultsAsync();
        }

        public async static Task CreateChild(IAsyncRepository<Business.Person> asyncRepository, Business.Person selected, Business.Person pair, Business.Person child)
        {
            var client = new GraphClient(
                new Uri(DatabaseInfo.Uri),
                DatabaseInfo.UserName,
                DatabaseInfo.Password);

            await client.ConnectAsync();

            await client.Cypher
                .Match("(selected:Person)-[:PARTNER_OF]->(pair:Person)")
                .Where<Business.Person>(p => p.ID == selected.ID)
                .With("selected, pair")
                .Create("(selected)<-[:CHILD_OF]-(child:Person)-[:CHILD_OF]->(pair)")
                .Set("child.ID = apoc.create.uuid()")
                .Set($"child.FirstName = '{ child.FirstName }'")
                .Set($"child.LastName = '{ child.LastName }'")
                .Set($"child.DateOfBirth = datetime('{ child.DateOfBirth:yyyy-MM-dd}')")
                .Set($"child.DateOfDeath = datetime('{ child.DateOfDeath:yyyy-MM-dd}')")
                .Set($"child.Gender = '{ child.Gender }'")
                .Set($"child.ImagePath = '{ child.Image.BaseUri }'")
                .With("child")
                .Match("(child)-[:CHILD_OF]->(mother:Person)")
                .Where($"mother.Gender = '{ GenderType.Female }'")
                .With("child, mother")
                .Create("(child)<-[:MOTHER_OF]-(mother)")
                .With("child")
                .Match("(child)-[:CHILD_OF]->(father:Person")
                .Where($"father.Gender = '{ GenderType.Male }'")
                .With("child, father")
                .Create("(child)<-[:FATHER_OF]-(father)")
                .ExecuteWithoutResultsAsync();
        }
    }
}
