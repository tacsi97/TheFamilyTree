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

        public void PostOrder(Business.Person person)
        {
            if (person == null) return;

            PostOrder(person.LeftmostChild);
            Visit(person);
            PostOrder(person.RightSibling);
        }

        public void Visit(Business.Person person)
        {

        }

        public IEnumerable<Node> InitializeNodes(IEnumerable<Business.Person> people)
        {
            var nodes = new List<Node>();

            foreach (var person in people)
            {

                if (person.Children != null)
                {
                    person.LeftmostChild.Node = new Node(person.Children.ElementAt(0));
                    nodes.Add(person.Node);
                }

                if (person.Father != null)
                {
                    var currentPersonIndex = 0; // person.Father.Children.IndexOf(person);

                    if (currentPersonIndex != 0)
                    {
                        person.RightSibling.Node = new Node(person.Father.Children.ElementAt(currentPersonIndex - 1));
                        nodes.Add(person.Node);
                    }
                }
            }

            return nodes;
        }
    }
}
