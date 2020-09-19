﻿using FamilyTree.Core;
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
        public override string RequestUriBase => Uris.BaseURI;
        public override string PostUri => Uris.PersonURI;
        public override string GetUri => Path.Combine(Uris.PersonURI, "{0}");
        public override string PutUri => Uris.PersonURI;
        public override string DeleteUri => Path.Combine(Uris.PersonURI, "{0}");
        public override string GetAllUri => Uris.PersonURI;
    }
}
