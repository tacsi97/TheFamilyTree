using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Services.Repository.Interfaces
{
    public interface IAsyncRepository<TObject>
    {
        // TODO: refactor after navigation branch IAsyncRemoteRepository(
        //                  Uri uri, 
        //                  Token(string userName, string password),
        //                  string content | int id)
        // Ebből lehet származtatni a graph repository-t is
        Task<IEnumerable<TObject>> GetAllAsync(string uri);

        Task<TObject> GetAsync(string uri, int id);

        Task CreateAsync(string uri, string content);

        Task ModifyAsync(string uri, string content);

        Task DeleteAsync(string uri, int id);
    }
}
