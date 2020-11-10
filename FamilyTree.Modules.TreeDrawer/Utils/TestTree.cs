using FamilyTree.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows;
using System.Xml.Linq;

namespace FamilyTree.Modules.TreeDrawer.Utils
{
    public class TestTree
    {
        public TestTree()
        {

        }

        public void PostOrder(Node node)
        {
            if (node == null) return;

            PostOrder(node.LeftMostChild);
            Visit(node);
            PostOrder(node.RightSibling);
        }

        public void Visit(Node node)
        {

        }

        public IEnumerable<Node> InitializeNodes(IEnumerable<Business.Person> people)
        {
            var nodes = new List<Node>();

            foreach (var person in people)
            {
                var node = new Node(person);

                if (person.Children != null)
                {
                    node.LeftMostChild = new Node(person.Children.ElementAt(0));
                    nodes.Add(node);
                }

                if (person.Father != null)
                {
                    var currentPersonIndex = person.Father.Children.IndexOf(person);

                    if (currentPersonIndex != 0)
                    {
                        node.RightSibling = new Node(person.Father.Children.ElementAt(currentPersonIndex - 1));
                        nodes.Add(node);
                    }
                }
            }

            return nodes;
        }
    }
}
