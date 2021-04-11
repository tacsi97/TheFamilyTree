using FamilyTree.Business;
using FamilyTree.Core;
using FamilyTree.Services.Repository;
using System.IO;
using System.Net.Http;

namespace FamilyTree.Modules.FamilyTree.Repository
{
    public class FamilyTreeRepository : HttpRepositoryBase<Business.FamilyTree>
    {
        public FamilyTreeRepository(string uri, Token token, HttpClient httpClient)
            : base(uri, token, httpClient)
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
