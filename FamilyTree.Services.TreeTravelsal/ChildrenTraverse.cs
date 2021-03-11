using FamilyTree.Business;
using FamilyTree.Services.TreeTravelsal.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace FamilyTree.Services.TreeTravelsal
{
    public class ChildrenTraverse : ITreeTraversal<Business.Node, Business.Line>
    {
        private double _leftmostValue = 0d;

        public ICollection<Node> Nodes { get; set; }
        public ICollection<Line> Lines { get; set; }

        public ChildrenTraverse()
        {
            Nodes = new ObservableCollection<Node>();
            Lines = new ObservableCollection<Line>();
        }

        // TODO: Az emebereken iterálunk végig, így jó lenne ha az emberben lenne egy hivatkozás a node ra, ami tartalmazza.
        // mivel így a node-nak is kellenek azok az attribútumok, mint pl leftmostchildren, mother, father...
        public void PostOrder(Node node)
        {
            if (node == null || node.Person == null) return;

            for (int i = 1; i < node.Person.Children.Count; i++)
            {
                node.Person.Children.ElementAt(i - 1).RightSibling = node.Person.Children.ElementAt(i);
                if (node.Person.Gender.Equals(GenderType.Male))
                    node.Father = node;
                else
                    node.Mother = node;
            }

            node.LeftMostChild = new Node(node.Person.Children.FirstOrDefault());
            if (node.Person.Gender.Equals(GenderType.Male))
                node.Father = node;
            else
                node.Mother = node;
            PostOrder(node.LeftMostChild);

            Visit(node);

            if (node.LeftMostChild.Person == null)
            {
                node.LeftCoordinate = _leftmostValue;
                _leftmostValue += node.Width + 25;
            }
            else
            {
                // (x * (y + u)) / 2 - (z*(y + u)) / 2
                // ahol
                // x = gyerekek száma
                // y = node szélessége
                // u = node közötti hely
                // z = szülők száma
                var x = node.Person.Children.Count;
                var y = node.Width;
                var u = 25;
                int z = 1;

                if (node.Person.Partner != null)
                {
                    node.Partner = new Node(node.Person.Partner);
                    z = 2;
                    node.LeftCoordinate = node.LeftMostChild.LeftCoordinate + (x * (y + u)) / 2 - (z * (y + u)) / 2;
                    node.TopCoordinate = node.LeftMostChild.TopCoordinate - (node.Height + 25);

                    node.Partner.LeftCoordinate = node.RigthCoordinate + 25;
                    node.Partner.TopCoordinate = node.TopCoordinate;

                    Nodes.Add(node.Partner);
                }
                else
                {
                    node.LeftCoordinate = node.LeftMostChild.LeftCoordinate + (x * (y + u)) / 2 - (z * (y + u)) / 2;
                    node.TopCoordinate = node.LeftMostChild.TopCoordinate - (node.Height + 25);
                }
            }

            node.RightSibling = new Node(node.Person.RightSibling);
            PostOrder(node.RightSibling);
        }

        public void PreOrder(Node node)
        {
            throw new NotImplementedException();
        }

        public void Visit(Node node)
        {
            Nodes.Add(node);
        }

        public void CreateArc(Business.Node upper, Business.Node lower)
        {
            var line = new Line();

            line.TopCoordinate = upper.BottomCoordinate;
            line.LeftCoordinate = (upper.LeftCoordinate + upper.RigthCoordinate) * 0.5;
            line.BottomCoordinate = lower.TopCoordinate;
            line.RigthCoordinate = (lower.LeftCoordinate + lower.RigthCoordinate) * 0.5;

            Lines.Add(line);
        }

        public void CreateArc(Business.Node mother, Business.Node father, Business.Node child)
        {
            // node|---|node
            Lines.Add(new Line()
            {
                TopCoordinate = (mother.TopCoordinate + mother.BottomCoordinate) * 0.5,
                LeftCoordinate = mother.RigthCoordinate,
                BottomCoordinate = (father.TopCoordinate + father.BottomCoordinate) * 0.5,
                RigthCoordinate = father.LeftCoordinate
            });

            // | 
            Lines.Add(new Line()
            {
                TopCoordinate = (mother.TopCoordinate + mother.BottomCoordinate) * 0.5,
                LeftCoordinate = (mother.RigthCoordinate + father.LeftCoordinate) * 0.5,
                BottomCoordinate = mother.BottomCoordinate - (25 * 0.5),
                RigthCoordinate = (mother.RigthCoordinate + father.LeftCoordinate) * 0.5
            });

            // ---
            Lines.Add(new Line()
            {
                TopCoordinate = mother.BottomCoordinate - (25 * 0.5),
                LeftCoordinate = (mother.RigthCoordinate + father.LeftCoordinate) * 0.5,
                BottomCoordinate = mother.BottomCoordinate - (25 * 0.5),
                RigthCoordinate = (child.LeftCoordinate + child.RigthCoordinate) * 0.5
            });

            // |
            Lines.Add(new Line()
            {
                TopCoordinate = mother.BottomCoordinate - (25 * 0.5),
                LeftCoordinate = (child.LeftCoordinate + child.RigthCoordinate) * 0.5,
                BottomCoordinate = child.TopCoordinate,
                RigthCoordinate = (child.LeftCoordinate + child.RigthCoordinate) * 0.5
            });
        }
    }
}
