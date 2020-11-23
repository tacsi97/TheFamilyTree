using FamilyTree.Core;
using FamilyTree.Services.Repository.Interfaces;
using Neo4jClient;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Core.Extensions
{
    public static class PersonGraphRepositoryExtensions
    {
        public static async Task<IEnumerable<Business.Person>> GetPeopleIncludedIn(this IAsyncGraphRepository<Business.Person> asyncGraph, int familyTreeID)
        {
            var people = new List<Business.Person>();

            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                var result = await gc.Cypher
                    .Match("(:FamilyTree)-[:ROOT]->(p:Person)-[*]-(o:Person)")
                    .Return<Business.Person>("o")
                    .ResultsAsync;

                foreach (var person in result)
                {
                    people.Add(person);
                }

            }

            return people;
        }

        public static async Task CreateParent(this IAsyncGraphRepository<Business.Person> asyncGraphRepository, Business.Person child, Business.Person parent)
        {
            // fel lehetne használi a normál create függvényt is asyncGraphRepository.CreateAsync(parent);
            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                var fatherOrMother = (parent.Gender == Business.GenderType.Male) ? "FATHER" : "MOTHER";

                await asyncGraphRepository.CreateAsync(parent);

                await gc.Cypher
                    .Match("(child:Person)")
                    .Where($"child.ID = {child.ID}")
                    .Create("(child)-[relone:CHILD]->(parent:Person)")
                    .Create($"(child)<-[reltwo:{fatherOrMother}]-(parent)")
                    .Set($"relone.From = date('{child.DateOfBirth:yyyy-MM-dd}')")
                    .Set($"relone.To = date('{DateTime.MinValue:yyyy-MM-dd}')")
                    .Set($"reltwo.From = date('{child.DateOfBirth:yyyy-MM-dd}')")
                    .Set($"reltwo.To = date('{DateTime.MinValue:yyyy-MM-dd}')")
                    .ExecuteWithoutResultsAsync();
            }
        }

        public static async Task CreateChild(this IAsyncGraphRepository<Business.Person> asyncGraphRepository, Business.Person parent, Business.Person child)
        {
            // fel lehetne használi a normál create függvényt is asyncGraphRepository.CreateAsync(parent);
            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                var fatherOrMother = (parent.Gender == Business.GenderType.Male) ? "FATHER" : "MOTHER";

                await asyncGraphRepository.CreateAsync(child);

                await gc.Cypher
                    .Match("(parent:Person)")
                    .Where($"parent.ID = {parent.ID}")
                    .Create($"(child:Person)-[relone:{fatherOrMother}]->(parent)")
                    .Create("(child)<-[reltwo:CHILD]-(parent)")
                    .Set($"relone.From = date('{child.DateOfBirth:yyyy-MM-dd}')")
                    .Set($"relone.To = date('{DateTime.MinValue:yyyy-MM-dd}')")
                    .Set($"reltwo.From = date('{child.DateOfBirth:yyyy-MM-dd}')")
                    .Set($"reltwo.To = date('{DateTime.MinValue:yyyy-MM-dd}')")
                    .ExecuteWithoutResultsAsync();
            }
        }

    }
}
