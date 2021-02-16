﻿using FamilyTree.Business;
using FamilyTree.Services.Repository;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FamilyTree.Modules.TreeDrawer.Repository
{
    public class PersonRepository : HttpRepositoryBase<Business.Person>
    {
        public PersonRepository(string uri, Token token, HttpClient httpClient)
            : base(uri, token, httpClient)
        {
        }

        public override string RequestUriBase => "";

        public override string PostUri => "";

        public override string GetUri => "";

        public override string PutUri => "";

        public override string DeleteUri => "";

        public override string GetAllUri => "";
    }
}
