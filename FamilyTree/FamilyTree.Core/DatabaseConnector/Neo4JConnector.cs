using Neo4j.Driver;
using System;

namespace FamilyTree.Core.DatabaseConnector
{
    public abstract class Neo4JConnector : IDisposable
    {
        protected readonly IDriver _driver;

        public Neo4JConnector(string uri, string user, string password)
        {
        }

        public void Dispose()
        {
            _driver?.Dispose();
        }
    }
}
