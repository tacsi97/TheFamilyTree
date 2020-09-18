using FamilyTree.Core;
using FamilyTree.Services.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Modules.Person.Repository
{
    public class PersonRepository : HttpRepository<Business.Person>
    {
        public override string RequestUriBase { get => Uris.BaseURI;}
        public override string PostUri { get => Uris.PersonURI; }
        public override string GetUri { get => Path.Combine(Uris.PersonURI, "{0}"); }
        public override string PutUri { get => Uris.PersonURI; }
        public override string DeleteUri { get => Uris.PersonURI; }
        public override string GetAllUri { get => Uris.PersonURI; }
    }
}
