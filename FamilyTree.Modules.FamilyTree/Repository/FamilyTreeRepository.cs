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

        public override string RequestUriBase => string.Empty;

        public override string PostUri => string.Empty;

        public override string GetUri => string.Empty;

        public override string PutUri => string.Empty;

        public override string DeleteUri => string.Empty;

        public override string GetAllUri => string.Empty;
    }
}
