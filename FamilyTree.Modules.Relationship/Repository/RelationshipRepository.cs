using FamilyTree.Business;
using FamilyTree.Core;
using FamilyTree.Services.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace FamilyTree.Modules.Relationship.Repository
{
    public class RelationshipRepository : HttpRepository<Business.Relationship>
    {
        public RelationshipRepository(string uri, Token token, HttpClient httpClient)
            : base(uri, token, httpClient)
        {
        }

        public override string RequestUriBase => Uris.BaseURI;

        public override string PostUri => Uris.RelationshipsURI;

        public override string GetUri => Path.Combine(Uris.RelationshipsURI, "{0}");

        public override string PutUri => Uris.RelationshipsURI;

        public override string DeleteUri => Path.Combine(Uris.RelationshipsURI, "{0}");

        public override string GetAllUri => Uris.RelationshipsURI;
    }
}
