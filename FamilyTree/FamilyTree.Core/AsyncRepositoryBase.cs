using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Core
{
    public abstract class AsyncRepositoryBase<TResult, TObject> : IAsyncRepository<TResult, TObject>
    {
        public abstract Task<TResult> Create(TObject templateObject);
        public abstract Task<TResult> Delete(int id);
        public abstract Task<TResult> Get(int id);
        public abstract Task<TResult> GetAll();
        public abstract Task<TResult> Modify(TObject templateObject);
    }
}
