using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTree.Core.DatabaseConnector
{
    public abstract class Neo4JConnector : IDisposable
    {
        private readonly IDriver _driver;

        public Neo4JConnector(string uri, string user, string password)
        {
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
        }

        public void Dispose()
        {
            _driver?.Dispose();
        }
    }
}
