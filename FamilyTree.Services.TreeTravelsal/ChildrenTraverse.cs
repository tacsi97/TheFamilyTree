using FamilyTree.Business;
using FamilyTree.Services.TreeTravelsal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FamilyTree.Services.TreeTravelsal
{
    public class ChildrenTraverse : ITreeTraversal<Business.Node, Business.Line>
    {
        public string sum = "";

        public ICollection<Node> Nodes { get; set; }
        public ICollection<Line> Lines { get; set; }

        public void PostOrder(Node node)
        {
            if (node == null || node.Person == null) return;

            for (int i = 1; i < node.Person.Children.Count; i++)
            {
                node.Person.Children.ElementAt(i - 1).RightSibling = node.Person.Children.ElementAt(i);
            }

            node.LeftMostChild = new Node(node.Person.Children.ElementAt(0));
            PostOrder(node.LeftMostChild);

            node.Partner = new Node(node.Person.Partner);
            PostOrder(node.Partner);

            node.RightSibling = new Node(node.Person.RightSibling);
            PostOrder(node.RightSibling);

            Visit(node);
        }

        public void PreOrder(Node node)
        {
            throw new NotImplementedException();
        }

        public void Visit(Node node)
        {
            sum += node.Person.FirstName + ", ";
        }
    }
}
