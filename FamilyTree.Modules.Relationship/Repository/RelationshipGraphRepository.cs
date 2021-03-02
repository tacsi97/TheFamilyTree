using FamilyTree.Business;
using FamilyTree.Core;
using FamilyTree.Modules.Relationship.Core;
using FamilyTree.Services.Repository.Interfaces;
using Neo4j.Driver;
using Neo4jClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Modules.Relationship.Repository
{
    public class RelationshipGraphRepository : IAsyncRepository<Business.Relationship>
    {
        public RelationshipGraphRepository(string uri, string user, string password)
        {
        }

        public Token Token { get; set; }
        public string Uri { get; set; }

        public async Task<Business.Relationship> CreateAsync(Business.Relationship template)
        {
            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                await gc.Cypher
                    .Match("(p1:Person)")
                    .Where($"p1.ID = {template.PersonFrom.ID}")
                    .Match("(p2:Person)")
                    .Where($"p2.ID = {template.PersonTo.ID}")
                    .Create("(p1)-[rel:")
                    .ExecuteWithoutResultsAsync();

                // TODO: Lekérdezni az előzőleg beszúrt emberkét
                return new Business.Relationship();
                #region Comment
                // hozzáadjuk az ember a fához
                //await gc.Cypher
                //    .Match("(ft:FamilyTree)<-[:IS_INCLUDED]-(fromPerson:Person)")
                //    .Where($"fromPerson.ID = { template.PersonFrom.ID }")
                //    .Create("(p:Person)-[:IS_INCLUDED]->(ft)")
                //    .Set($"p.ID = { template.PersonTo.ID }")
                //    .Set($"p.FirstName = '{ template.PersonTo.FirstName }'")
                //    .Set($"p.LastName = '{ template.PersonTo.LastName }'")
                //    .Set($"p.DateOfBirth = date('{ template.PersonTo.DateOfBirth:yyyy-MM-dd}')")
                //    .Set($"p.DateOfDeath = date('{ template.PersonTo.DateOfDeath:yyyy-MM-dd}')")
                //    .Set($"p.Gender = '{ template.PersonTo.Gender }'")
                //    .ExecuteWithoutResultsAsync();

                //await gc.Cypher
                //    .Match("(one:Person)")
                //    .Where($"one.ID = { template.PersonFrom.ID }")
                //    .Match("(another:Person)")
                //    .Where($"another.ID = { template.PersonTo.ID }")
                //    .Create("(one)<-[:FROM]-(rel:Relationship)-[:TO]->(another)")
                //    .Set($"rel.ID = {GlobalID.NewID()}")
                //    .Set($"rel.From = date('{template.From:yyyy-MM-dd}')")
                //    .Set($"rel.To = date('{template.To:yyyy-MM-dd}')")
                //    .Set($"rel.RelationType = '{template.RelationType}'")
                //    .ExecuteWithoutResultsAsync();

                //// ha szülő
                //if (template.RelationType.Equals(TypeNames.Parent))
                //{
                //    // apa
                //    if (template.PersonTo.Gender.Equals(GenderType.Male))
                //    {
                //        await gc.Cypher
                //            .Match("(father:Person)")
                //            .Where($"father.ID = { template.PersonTo.ID }")
                //            .Match("(child:Person)")
                //            .Where($"child.ID = { template.PersonFrom.ID }")
                //            .Create("(father)-[:FATHER_OF]->(child)")
                //            .ExecuteWithoutResultsAsync();
                //    }
                //    // anya
                //    else
                //    {
                //        await gc.Cypher
                //            .Match("(mother:Person)")
                //            .Where($"mother.ID = { template.PersonTo.ID }")
                //            .Match("(child:Person)")
                //            .Where($"child.ID = { template.PersonFrom.ID }")
                //            .Create("(mother)-[:MOTHER_OF]->(child)")
                //            .ExecuteWithoutResultsAsync();
                //    }
                //}
                //// partner
                //else if (template.RelationType.Equals(TypeNames.Partner))
                //{
                //    await gc.Cypher
                //            .Match("(one:Person)")
                //            .Where($"one.ID = { template.PersonTo.ID }")
                //            .Match("(another:Person)")
                //            .Where($"another.ID = { template.PersonFrom.ID }")
                //            .Create("(one)-[rel:IN_RELATIONSHIP]->(another)")
                //            .Set($"rel.ID = { template.ID }")
                //            .Set($"rel.From = date('{template.From:yyyy-MM-dd}')")
                //            .Set($"rel.To = date('{template.To:yyyy-MM-dd}')")
                //            .ExecuteWithoutResultsAsync();
                //}
                //else
                //{
                //    // apa
                //    if (template.PersonTo.Gender.Equals(GenderType.Male))
                //    {
                //        await gc.Cypher
                //            .Match("(father:Person)")
                //            .Where($"father.ID = { template.PersonFrom.ID }")
                //            .Match("(child:Person)")
                //            .Where($"child.ID = { template.PersonTo.ID }")
                //            .Create("(father)-[:FATHER_OF]->(child)")
                //            .ExecuteWithoutResultsAsync();
                //    }
                //    // anya
                //    else
                //    {
                //        await gc.Cypher
                //            .Match("(mother:Person)")
                //            .Where($"mother.ID = { template.PersonFrom.ID }")
                //            .Match("(child:Person)")
                //            .Where($"child.ID = { template.PersonTo.ID }")
                //            .Create("(mother)-[:MOTHER_OF]->(child)")
                //            .ExecuteWithoutResultsAsync();
                //    }
                //}

                #endregion
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                await gc.Cypher
                    .Match("(rel:Relationship)")
                    .Where($"rel.ID = { id }")
                    .DetachDelete("rel")
                    .ExecuteWithoutResultsAsync();
            }
        }

        public async Task<IEnumerable<Business.Relationship>> GetAllAsync()
        {
            var list = new List<Business.Relationship>();

            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                var result = await gc.Cypher
                    .Match("(rel:Relationship)")
                    .Return<Business.Relationship>("rel")
                    .ResultsAsync;

                foreach (var relationship in result)
                {
                    list.Add(relationship);
                }
            }

            return list;
        }

        public async Task<Business.Relationship> GetAsync(int id)
        {
            Business.Relationship relationship = null;

            using (var gc = new GraphClient(
                new Uri(DatabaseInfo.Uri),
                DatabaseInfo.UserName,
                DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                var result = await gc.Cypher
                    .Match("(rel:Relationship)")
                    .Where($"rel.ID = { id }")
                    .Return<Business.Relationship>("rel")
                    .ResultsAsync;

                foreach (var rel in result)
                {
                    relationship = rel;
                }
            }

            return relationship;
        }

        public async Task ModifyAsync(Business.Relationship template)
        {
            using (var gc = new GraphClient(new Uri(DatabaseInfo.Uri), DatabaseInfo.UserName, DatabaseInfo.Password))
            {
                await gc.ConnectAsync();

                await gc.Cypher
                    .Match("(rel:Relationship)")
                    .Where($"rel.ID = { template.ID }")
                    .Set($"rel.From = date('{template.From:yyyy-MM-dd}')")
                    .Set($"rel.To = date('{template.To:yyyy-MM-dd}')")
                    .ExecuteWithoutResultsAsync();
            }
        }
    }
}
