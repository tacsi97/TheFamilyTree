using FamilyTree.Services.TreeTravelsal.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTree.Services.TreeTravelsal
{
    public class TreeTravelsalText : ITreeTravelsalText<Business.Node>
    {
        #region Properties

        public StringBuilder Builder { get; set; }

        public Business.Node Root { get; set; }

        #endregion

        public TreeTravelsalText()
        {
            Builder = new StringBuilder();
        }

        public void PostOrder(Business.Node node)
        {
            if (node == null) return;
            PostOrder(node.Mother);
            PostOrder(node.Father);
            Visit(node);
        }

        public void PreOrder(Business.Node node)
        {
            throw new NotImplementedException();
        }

        public void Visit(Business.Node node)
        {
            Builder.Append(node.Person.FirstName);
            Builder.Append(" ");
            Builder.Append(node.Person.LastName);
            Builder.Append(", ");
        }
    }
}
