using FamilyTree.Services.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Services.Repository
{
    public abstract class AsyncRepositoryBase<TResult> : IAsyncRepository<TResult>
    {
        public abstract Task<TResult> CreateAsync(string uri, string content);
        public abstract Task<TResult> DeleteAsync(string uri, int id);
        public abstract Task<TResult> GetAsync(string uri, int id);
        public abstract Task<TResult> GetAllAsync(string uri);
        public abstract Task<TResult> ModifyAsync(string uri, string content);
    }
}
