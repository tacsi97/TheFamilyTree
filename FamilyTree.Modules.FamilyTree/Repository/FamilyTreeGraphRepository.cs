using FamilyTree.Core;
using FamilyTree.Core.Extensions;
using FamilyTree.Services.Repository;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.Modules.FamilyTree.Repository
{
    public class FamilyTreeGraphRepository : LocalRepositoryBase<Business.FamilyTree>
    {
        private readonly GraphClient _graphClient;

        public FamilyTreeGraphRepository(string uri, Business.Token token)
            : base(uri, token)
        {
            _graphClient = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password);
            _graphClient.ConnectAsync().FireAndForgetAsync();
        }

        public override async Task<Business.FamilyTree> CreateAsync(Business.FamilyTree template)
        {
            var result = await _graphClient.Cypher
                .Create("(ft:FamilyTree)<-[:MEMBER_OF]-(person:Person)")
                .Set($"ft.{ nameof(template.Name) } = '{ template.Name }'")
                .Set($"ft.{ nameof(template.ID) } = apoc.create.uuid()")
                .Set($"person.{ nameof(template.ID) } = apoc.create.uuid()")
                .Return<Business.FamilyTree>("ft")
                .ResultsAsync;

            var tree = result.First();

            var people = await _graphClient.Cypher
                .Match("(person:Person)-[:MEMBER_OF]->(ft:FamilyTree)")
                .Where($"ft.{ nameof(tree.ID)} = '{ tree.ID }'")
                .Return<Business.Person>("person")
                .ResultsAsync;

            tree.People.Add(people.First());

            return result.First();
        }

        public override async Task DeleteAsync(string id)
        {
            await _graphClient.Cypher
                .Match("(ft:FamilyTree)")
                .Where($"ft.ID = '{ id }'")
                .DetachDelete("ft")
                .ExecuteWithoutResultsAsync();

            await _graphClient.Cypher
                .Match("(p:Person)")
                .Where("NOT (p)--()")
                .Delete("p")
                .ExecuteWithoutResultsAsync();
        }

        public override async Task<IEnumerable<Business.FamilyTree>> GetAllAsync()
        {
            var result = await _graphClient.Cypher
                .Match("(ft:FamilyTree)")
                .Return<Business.FamilyTree>("ft")
                .ResultsAsync;

            var trees = result;

            foreach (var tree in result)
            {
                var people = await _graphClient.Cypher
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

        public override async Task<Business.FamilyTree> GetAsync(string id)
        {
            var result = await _graphClient.Cypher
                .Match("(ft:FamilyTree)")
                .Where($"ft.ID = '{ id }'")
                .Return<Business.FamilyTree>("ft")
                .ResultsAsync;

            var tree = result.First();

            var people = await _graphClient.Cypher
                .Match("(person:Person)-[:MEMBER_OF]->(ft:FamilyTree)")
                .Where($"ft.{ nameof(tree.ID)} = '{ tree.ID }'")
                .Return<Business.Person>("person")
                .ResultsAsync;

            tree.People.Add(people.First());

            return tree;
        }

        public override async Task ModifyAsync(Business.FamilyTree template)
        {
            await _graphClient.Cypher
                .Match("(ft:FamilyTree)")
                .Where($"ft.{ nameof(template.ID) } = '{ template.ID }'")
                .Set($"ft.{ nameof(template.Name) } = '{ template.Name }'")
                .ExecuteWithoutResultsAsync();
        }
    }
}
