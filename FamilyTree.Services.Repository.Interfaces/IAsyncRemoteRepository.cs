using FamilyTree.Business;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Services.Repository.Interfaces
{
    public interface IAsyncRemoteRepository<T>
    {
        public Token Token { get; set; }

        public string Uri { get; set; }

        // TODO: refactor after navigation branch IAsyncRemoteRepository(
        //                  Uri uri, 
        //                  Token(string userName, string password),
        //                  string content | int id)
        // Ebből lehet származtatni a graph repository-t is
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(int id);

        Task CreateAsync(T content);

        Task ModifyAsync(T content);

        Task DeleteAsync(int id);
    }
}
