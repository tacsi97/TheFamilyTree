using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTree.Core
{
    public class Uris
    {
        public const string Neo4jUri = "neo4j://localhost:7687";

        public const string BaseURI = "http://localhost:5000/api";
        
        public const string PersonURI = "http://localhost:5000/api/person";

        public const string FamilyTreeURI = "http://localhost:5000/api/familytree";

        public const string RelationshipsURI = "http://localhost:5000/api/relationship";
    }
}
