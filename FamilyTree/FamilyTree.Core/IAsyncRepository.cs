using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Core
{
    public interface IAsyncRepository<TResult, TObject>
    {
        Task<TResult> GetAll();

        Task<TResult> Get(int id);

        Task<TResult> Create(TObject templateObject);

        Task<TResult> Modify(TObject templateObject);

        Task<TResult> Delete(int id);
    }
}
