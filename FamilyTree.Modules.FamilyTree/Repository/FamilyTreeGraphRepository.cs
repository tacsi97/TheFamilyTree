using FamilyTree.Core;
using FamilyTree.Services.Repository;
using FamilyTree.Services.Repository.Interfaces;
using Neo4j.Driver;
using Neo4jClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Modules.FamilyTree.Repository
{
    public class FamilyTreeGraphRepository : LocalRepositoryBase<Business.FamilyTree>
    {
        public FamilyTreeGraphRepository(string uri, Business.Token token)
            : base(uri, token)
        {
        }

        public override async Task<Business.FamilyTree> CreateAsync(Business.FamilyTree template)
        {
            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                var result = await gc.Cypher
                    .Create("(ft:FamilyTree)<-[:MEMBER_OF]-(person:Person)")
                    .Set($"ft.{ nameof(template.Name) } = '{ template.Name }'")
                    .Set($"ft.{ nameof(template.ID) } = apoc.create.uuid()")
                    .Set($"person.{ nameof(template.ID) } = apoc.create.uuid()")
                    .Return<Business.FamilyTree>("ft")
                    .ResultsAsync;

                var tree = result.First();

                var people = await gc.Cypher
                    .Match("(person:Person)-[:MEMBER_OF]->(ft:FamilyTree)")
                    .Where($"ft.{ nameof(tree.ID)} = '{ tree.ID }'")
                    .Return<Business.Person>("person")
                    .ResultsAsync;

                tree.People.Add(people.First());

                return result.First();
            }
        }

        public override async Task DeleteAsync(string id)
        {
            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                await gc.Cypher
                    .Match("(ft:FamilyTree)")
                    .Where($"ft.ID = '{ id }'")
                    .DetachDelete("ft")
                    .ExecuteWithoutResultsAsync();
            }
        }

        public override async Task<IEnumerable<Business.FamilyTree>> GetAllAsync()
        {
            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                var result = await gc.Cypher
                    .Match("(ft:FamilyTree)")
                    .Return<Business.FamilyTree>("ft")
                    .ResultsAsync;

                var trees = result;

                foreach (var tree in result)
                {
                    var people = await gc.Cypher
                        .Match("(p:Person)-[:MEMBER_OF]->(ft:FamilyTree)")
                        .Where($"ft.ID = '{ tree.ID }'")
                        .Return<Business.Person>("p")
                        .ResultsAsync;

                    tree.People.Clear();

                    foreach (var person in people)
                    {
                        tree.People.Add(person);
                    }

                    trees.ToList().Add(tree);
                }

                return trees;
            }
        }

        public override async Task<Business.FamilyTree> GetAsync(string id)
        {
            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                var result = await gc.Cypher
                    .Match("(ft:FamilyTree)")
                    .Where($"ft.ID = '{ id }'")
                    .Return<Business.FamilyTree>("ft")
                    .ResultsAsync;

                var tree = result.First();

                var people = await gc.Cypher
                    .Match("(person:Person)-[:MEMBER_OF]->(ft:FamilyTree)")
                    .Where($"ft.{ nameof(tree.ID)} = '{ tree.ID }'")
                    .Return<Business.Person>("person")
                    .ResultsAsync;

                tree.People.Add(people.First());

                return tree;
            }
        }

        public override async Task ModifyAsync(Business.FamilyTree template)
        {
            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                await gc.Cypher
                    .Match("(ft:FamilyTree)")
                    .Where($"ft.{ nameof(template.ID) } = '{ template.ID }'")
                    .Set($"ft.{ nameof(template.Name) } = '{ template.Name }'")
                    .ExecuteWithoutResultsAsync();
            }
        }
    }
}
