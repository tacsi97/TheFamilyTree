using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTree.Services.TreeTravelsal.Interfaces
{
    public interface ITreeTraversal<T, V>
    {
        ICollection<T> Nodes { get; set; }

        ICollection<V> Lines { get; set; }

        void PostOrder(Business.Person person);

        void PreOrder(Business.Person person);

        void Visit(Business.Person person);
    }
}
