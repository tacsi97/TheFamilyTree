using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Services.Repository.Interfaces
{
    public interface IAsyncGraphRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(int id);

        Task CreateAsync(T template);

        Task ModifyAsync(T template);

        Task DeleteAsync(int id);
    }
}
