using FamilyTree.Services.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Services.Repository
{
    public abstract class AsyncRepositoryBase<TObject> : IAsyncRepository<TObject>
    {
        public abstract Task CreateAsync(string uri, string content);
        public abstract Task DeleteAsync(string uri, int id);
        public abstract Task<IEnumerable<TObject>> GetAllAsync(string uri);
        public abstract Task<TObject> GetAsync(string uri, int id);
        public abstract Task ModifyAsync(string uri, string content);
    }
}
