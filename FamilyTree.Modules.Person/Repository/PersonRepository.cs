using FamilyTree.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Modules.Person.Repository
{
    public class PersonRepository : HttpRepository<Business.Person>
    {
        public override string RequestUriBase { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string PostUri { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string GetUri { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string PutUri { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string DeleteUri { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string GetAllUri { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
