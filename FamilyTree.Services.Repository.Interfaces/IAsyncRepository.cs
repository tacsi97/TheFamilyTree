using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Services.Repository.Interfaces
{
    public interface IAsyncRepository<TObject>
    {
        Task<IEnumerable<TObject>> GetAllAsync(string uri);

        Task<TObject> GetAsync(string uri, int id);

        Task CreateAsync(string uri, string content);

        Task ModifyAsync(string uri, string content);

        Task DeleteAsync(string uri, int id);
    }
}
