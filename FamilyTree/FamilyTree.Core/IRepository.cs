using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Core
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        T Get(int id);

        bool Create(T templateObject);

        bool Modify(T templateObject);

        bool Delete(int id);
    }
}
