using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Services.Repository.Interfaces
{
    public interface IAsyncRepository<TResult>
    {
        Task<TResult> GetAllAsync(string uri);

        Task<TResult> GetAsync(string uri, int id);

        Task<TResult> CreateAsync(string uri, string content);

        Task<TResult> ModifyAsync(string uri, string content);

        Task<TResult> DeleteAsync(string uri, int id);
    }
}
