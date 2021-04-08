using FamilyTree.Business;
using FamilyTree.Core;
using FamilyTree.Services.Repository.Interfaces;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.Modules.Person.Extensions
{
    public static class GraphRepositoryExtensions
    {
        public async static Task MakePair(this IAsyncRepository<Business.Person> asyncRepository, Business.Person one, Business.Person another)
        {
            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                await gc.Cypher
                    .Match($"(one:Person {{ { nameof(one.ID) }: '{ one.ID }' }}), (another:Person {{ { nameof(another.ID) }: '{ another.ID }' }})")
                    .Merge("(one)-[:PARTNER_OF]->(another)")
                    .With("one, another")
                    .Create("(one)<-[:PARTNER_OF]-(another)")
                    .Set($"one.{ nameof(one.PartnerID) } = '{ another.ID }'")
                    .Set($"another.{ nameof(another.PartnerID) } = '{ one.ID }'")
                    .ExecuteWithoutResultsAsync();
            }
        }

        public async static Task<Business.Person> CreateMother(this IAsyncRepository<Business.Person> asyncRepository, Business.Person child, Business.Person mother)
        {
            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                var result = await gc.Cypher
                    .Match("(child:Person)")
                    .Where($"child.{ nameof(child.ID) } = '{ child.ID }'")
                    .Create("(child)<-[:MOTHER_OF]-(mother:Person)")
                    .Set("mother.ID = apoc.create.uuid()")
                    .Set($"mother.FirstName = '{ mother.FirstName }'")
                    .Set($"mother.LastName = '{ mother.LastName }'")
                    .Set($"mother.DateOfBirth = datetime('{ mother.DateOfBirth:yyyy-MM-dd}')")
                    .Set($"mother.DateOfDeath = datetime('{ mother.DateOfDeath:yyyy-MM-dd}')")
                    .Set($"mother.Gender = '{ mother.Gender }'")
                    .Set($"mother.ImagePath = '{ mother.ImagePath }'")
                    .Set($"child.{ nameof(child.MotherID) } = mother.ID")
                    .With("child, mother")
                    .Match("(ft:FamilyTree)<-[:MEMBER_OF]-(child)")
                    .Create("(ft)<-[:MEMBER_OF]-(mother)<-[:CHILD_OF]-(child)")
                    .Return<Business.Person>("mother")
                    .ResultsAsync;

                return result.FirstOrDefault();
            }
        }

        public async static Task<Business.Person> CreateFather(this IAsyncRepository<Business.Person> asyncRepository, Business.Person child, Business.Person father)
        {
            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                var result = await gc.Cypher
                    .Match("(child:Person)")
                    .Where($"child.{ nameof(child.ID) } = '{ child.ID }'")
                    .Create("(child)<-[:FATHER_OF]-(father:Person)")
                    .Set("father.ID = apoc.create.uuid()")
                    .Set($"father.FirstName = '{ father.FirstName }'")
                    .Set($"father.LastName = '{ father.LastName }'")
                    .Set($"father.DateOfBirth = datetime('{ father.DateOfBirth:yyyy-MM-dd}')")
                    .Set($"father.DateOfDeath = datetime('{ father.DateOfDeath:yyyy-MM-dd}')")
                    .Set($"father.Gender = '{ father.Gender }'")
                    .Set($"father.ImagePath = '{ father.ImagePath }'")
                    .Set($"child.{ nameof(child.FatherID) } = father.ID")
                    .With("child, father")
                    .Match("(ft:FamilyTree)<-[:MEMBER_OF]-(child)")
                    .Create("(ft)<-[:MEMBER_OF]-(father)<-[:CHILD_OF]-(child)")
                    .Return<Business.Person>("father")
                    .ResultsAsync;

                return result.FirstOrDefault();
            }
        }

        public async static Task CreatePair(this IAsyncRepository<Business.Person> asyncRepository, Business.Person selected, Business.Person pair)
        {
            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                await gc.Cypher
                    .Match("(selected:Person)")
                    .Where($"selected.{ nameof(selected.ID) } = '{ selected.ID }'")
                    .With("selected")
                    .Create("(selected)<-[:PARTNER_OF]-(pair:Person)")
                    .Set("pair.ID = apoc.create.uuid()")
                    .Set($"pair.FirstName = '{ pair.FirstName }'")
                    .Set($"pair.LastName = '{ pair.LastName }'")
                    .Set($"pair.DateOfBirth = datetime('{ pair.DateOfBirth:yyyy-MM-dd}')")
                    .Set($"pair.DateOfDeath = datetime('{ pair.DateOfDeath:yyyy-MM-dd}')")
                    .Set($"pair.Gender = '{ pair.Gender }'")
                    .Set($"pair.ImagePath = '{ pair.ImagePath }'")
                    .Set($"selected.{ nameof(selected.PartnerID) } = pair.ID")
                    .With("selected, pair")
                    .Create("(selected)-[:PARTNER_OF]->(pair)")
                    .With("selected, pair")
                    .Match("(selected)-[:MEMBER_OF]->(ft:FamilyTree)")
                    .Create("(pair)-[:MEMBER_OF]->(ft)")
                    .ExecuteWithoutResultsAsync();
            }
        }

        // TODO: ezt lehetne konkrétan is megadni, mert tudjuk ki az apa és anya, így nem kell a query-ben kideríteni...
        public async static Task CreateChild(this IAsyncRepository<Business.Person> asyncRepository, Business.Person selected, Business.Person pair, Business.Person child)
        {
            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                await gc.Cypher
                    .Match("(selected:Person)-[:PARTNER_OF]->(pair:Person)")
                    .Where($"selected.{ nameof(selected.ID) } = '{ selected.ID }'")
                    .With("selected, pair")
                    .Create("(selected)<-[:CHILD_OF]-(child:Person)-[:CHILD_OF]->(pair)")
                    .Set("child.ID = apoc.create.uuid()")
                    .Set($"child.FirstName = '{ child.FirstName }'")
                    .Set($"child.LastName = '{ child.LastName }'")
                    .Set($"child.DateOfBirth = datetime('{ child.DateOfBirth:yyyy-MM-dd}')")
                    .Set($"child.DateOfDeath = datetime('{ child.DateOfDeath:yyyy-MM-dd}')")
                    .Set($"child.Gender = '{ child.Gender }'")
                    .Set($"child.ImagePath = '{ child.ImagePath }'")
                    .With("child")
                    .Match("(child)-[:CHILD_OF]->(mother:Person)")
                    .Where($"mother.Gender = '{ GenderType.Female }'")
                    .With("child, mother")
                    .Create("(child)<-[:MOTHER_OF]-(mother)")
                    .Set($"child.{ nameof(child.MotherID) } = mother.ID")
                    .With("child")
                    .Match("(child)-[:CHILD_OF]->(father:Person)")
                    .Where($"father.Gender = '{ GenderType.Male }'")
                    .With("child, father")
                    .Create("(child)<-[:FATHER_OF]-(father)")
                    .Set($"child.{ nameof(child.FatherID) } = father.ID")
                    .With("child, father")
                    .Match("(father)-[:MEMBER_OF]->(ft:FamilyTree)")
                    .With("child, ft")
                    .Create("(child)-[:MEMBER_OF]->(ft)")
                    .ExecuteWithoutResultsAsync();
            }
        }

        public async static Task<IEnumerable<Business.Person>> GetPeopleAsync(this IAsyncRepository<Business.Person> asyncRepository, Business.FamilyTree tree)
        {
            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                return await gc.Cypher
                    .Match("(ft:FamilyTree)<-[:MEMBER_OF]-(p:Person)")
                    .Where($"ft.{ nameof(tree.ID) } = '{ tree.ID }'")
                    .ReturnDistinct<Business.Person>("p")
                    .ResultsAsync;
            }
        }
    }
}
