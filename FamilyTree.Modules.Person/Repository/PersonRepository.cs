using FamilyTree.Business;
using FamilyTree.Core;
using FamilyTree.Services.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Modules.Person.Repository
{
    /// <summary>
    /// <inheritdoc cref="HttpRepositoryBase{TObject}"/>
    /// </summary>
    public class PersonRepository : HttpRepositoryBase<Business.Person>
    {
        public PersonRepository(string uri, Token token, HttpClient httpClient)
            : base(uri, token, httpClient)
        {
        }

        public override string RequestUriBase => Uris.BaseURI;
        public override string PostUri => Uris.PersonURI;
        public override string GetUri => Path.Combine(Uris.PersonURI, "{0}");
        public override string PutUri => Uris.PersonURI;
        public override string DeleteUri => Path.Combine(Uris.PersonURI, "{0}");
        public override string GetAllUri => Uris.PersonURI;
    }
}
