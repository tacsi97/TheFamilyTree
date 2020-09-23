using FamilyTree.Business;
using FamilyTree.Core;
using FamilyTree.Services.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Text;

namespace FamilyTree.Modules.FamilyTree.Repository
{
    public class FamilyTreeRepository : HttpRepository<Business.FamilyTree>
    {
        public FamilyTreeRepository(HttpClient httpClient) : base(httpClient)
        {
        }

        public override string RequestUriBase => Uris.BaseURI;

        public override string PostUri => Uris.FamilyTreeURI;

        public override string GetUri => Path.Combine(Uris.FamilyTreeURI, "{0}");

        public override string PutUri => Uris.FamilyTreeURI;

        public override string DeleteUri => Path.Combine(Uris.FamilyTreeURI, "{0}");

        public override string GetAllUri => Uris.FamilyTreeURI;
    }
}
