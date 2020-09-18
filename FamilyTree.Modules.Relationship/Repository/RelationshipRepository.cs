using FamilyTree.Core;
using FamilyTree.Services.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FamilyTree.Modules.Relationship.Repository
{
    public class RelationshipRepository : HttpRepository<Business.Relationship>
    {
        public override string RequestUriBase => Uris.BaseURI;

        public override string PostUri => Uris.RelationshipsURI;

        public override string GetUri => Path.Combine(Uris.RelationshipsURI, "{0}");

        public override string PutUri => Uris.RelationshipsURI;

        public override string DeleteUri => Uris.RelationshipsURI;

        public override string GetAllUri => Uris.RelationshipsURI;
    }
}
