using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTree.Services.TreeTravelsal.Interfaces
{
    public interface ITreeTraversal<T>
    {
        ICollection<T> Nodes { get; set; }

        void PostOrder(T node);

        void PreOrder(T node);

        void Visit(T node);
    }
}
