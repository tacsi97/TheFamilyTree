using FamilyTree.Core;
using FamilyTree.Services.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Modules.Person.Repository
{
    public class PersonRepository : HttpRepository<Business.Person>
    {
        public override string RequestUriBase { get =>  "asd"; set => throw new NotImplementedException(); }
        public override string PostUri { get =>         "asd"; set => throw new NotImplementedException(); }
        public override string GetUri { get =>          "asd"; set => throw new NotImplementedException(); }
        public override string PutUri { get =>          "asd"; set => throw new NotImplementedException(); }
        public override string DeleteUri { get =>       "asd"; set => throw new NotImplementedException(); }
        public override string GetAllUri { get =>       "asd"; set => throw new NotImplementedException(); }
    }
}
