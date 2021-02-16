using FamilyTree.Core;
using FamilyTree.Services.Repository;
using FamilyTree.Services.Repository.Interfaces;
using Neo4j.Driver;
using Neo4jClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public override async Task CreateAsync(Business.FamilyTree template)
        {
            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                await gc.Cypher
                    .Create("(ft:FamilyTree)")
                    .Set($"ft.Name = '{ template.Name }'")
                    .Set($"ft.ID = { template.ID }")
                    .Create("(pair:Person)<-[:TO]-(rel:Relationship)-[:FROM]->(p:Person)-[:IS_INCLUDED]->(ft)")
                    .Set($"p.ID = { GlobalID.NewID() }")
                    .Set($"pair.ID = { GlobalID.NewID() }")
                    .Set($"rel.ID = {GlobalID.NewID()}")
                    .ExecuteWithoutResultsAsync();
            }
        }

        public override async Task DeleteAsync(int id)
        {
            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                await gc.Cypher
                    .Match("(ft:FamilyTree)")
                    .Where($"ft.ID = { id }")
                    .DetachDelete("ft")
                    .ExecuteWithoutResultsAsync();
            }
        }

        public override async Task<IEnumerable<Business.FamilyTree>> GetAllAsync()
        {
            var trees = new List<Business.FamilyTree>();

            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                var result = await gc.Cypher
                    .Match("(ft:FamilyTree)")
                    .Return<Business.FamilyTree>("ft")
                    .ResultsAsync;

                foreach (var tree in result)
                {
                    var people = await gc.Cypher
                        .Match("(p:Person)-[:IS_INCLUDED]->(ft:FamilyTree)")
                        .Where($"ft.ID = { tree.ID }")
                        .Return<Business.Person>("p")
                        .ResultsAsync;

                    tree.People.Clear();

                    foreach (var person in people)
                    {
                        tree.People.Add(person);
                    }

                    trees.Add(tree);
                }

                var resultOne = await gc.Cypher
                    .Match("(node)")
                    .Return<int>("count(node)")
                    .ResultsAsync;

                var resultTwo = await gc.Cypher
                    .Match("()-[rel:IN_RELATIONSHIP]->()")
                    .Return<int>("count(rel)")
                    .ResultsAsync;

                var numberOfIDs = 0;

                foreach (var integer in resultOne)
                {
                    numberOfIDs += integer;
                }

                foreach (var integer in resultTwo)
                {
                    numberOfIDs += integer;
                }

                GlobalID.SetID(numberOfIDs);
            }
            return trees;
        }

        public override async Task<Business.FamilyTree> GetAsync(int id)
        {
            Business.FamilyTree tree = null;

            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                var result = await gc.Cypher
                    .Match("(ft:FamilyTree)")
                    .Where($"ft.ID = '{ id }'")
                    .Return<Business.FamilyTree>("ft")
                    .ResultsAsync;

                tree = result.GetEnumerator().Current;
            }

            return tree;
        }

        public override async Task ModifyAsync(Business.FamilyTree template)
        {
            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                await gc.Cypher
                    .Match("(ft:FamilyTree)")
                    .Where($"ft.ID = { template.ID }")
                    .Set($"ft.Name = '{ template.Name }'")
                    .ExecuteWithoutResultsAsync();
            }
        }
    }
}
