using FamilyTree.Services.Repository.Interfaces;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Core.Extensions
{
    public static class RelationGraphRepositoryExtensions
    {
        public static async Task<IEnumerable<Business.Relationship>> GetRelationshipsIncludedIn(this IAsyncRepository<Business.Relationship> asyncGraph, int familyTreeID)
        {
            var relationships = new List<Business.Relationship>();

            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                var familyTree = await gc.Cypher
                    .Match("(:FamilyTree)-[:ROOT]->(p:Person)-[*]-(o:Person)")
                    .Return<Business.Person>("o")
                    .ResultsAsync; 

                var result = await gc.Cypher
                    .Match("(p1:Person)-[:IS_INCLUDED]->(ft:FamilyTree)")
                    .Where($"ft.ID = { familyTreeID }")
                    .Match("(p1)<-[:FROM]-(rel:Relationship)-[:TO]->(p2:Person)")
                    .Return<Business.Relationship>("rel")
                    .ResultsAsync;

                foreach (var relation in result)
                {
                    var personResult = await gc.Cypher
                    .Match("(p:Person)<-[:FROM]-(rel:Relationship)")
                    .Where($"rel.ID = { relation.ID }")
                    .Return<Business.Person>("p")
                    .ResultsAsync;

                    var personFrom = personResult.First();

                    personResult = await gc.Cypher
                    .Match("(p:Person)<-[:TO]-(rel:Relationship)")
                    .Where($"rel.ID = { relation.ID }")
                    .Return<Business.Person>("p")
                    .ResultsAsync;

                    var personTo = personResult.First();

                    relation.PersonFrom = personFrom;
                    relation.PersonTo = personTo;
                    //relation.PersonFrom.FamilyTree = familyTree;

                    relationships.Add(relation);
                }
            }

            return relationships;
        }

        public static async Task CreateParentRelationship(this IAsyncRepository<Business.Relationship> asyncGraphRepository, Business.Person fromPerson, Business.Person toPerson, DateTime from, DateTime to)
        {
            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {

            }
        }
    }
}
