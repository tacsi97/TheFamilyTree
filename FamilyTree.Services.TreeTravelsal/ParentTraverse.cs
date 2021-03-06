using FamilyTree.Business;
using FamilyTree.Services.TreeTravelsal.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTree.Services.TreeTravelsal
{
    public class ParentTraverse : ITreeTraversal<Business.Node, Business.Line>
    {
        #region Fields

        private double _leftmostValue = 0d;

        #endregion

        #region Properties

        public ICollection<Business.Node> Nodes { get; set; }

        public ICollection<Business.Line> Lines { get; set; }

        #endregion

        public ParentTraverse()
        {
            Nodes = new List<Business.Node>();
            Lines = new List<Business.Line>();
        }

        public void PostOrder(Business.Node node)
        {
            // TODO: Vagy itt kötögetem össze az embereket, vagy egy másik fa bejárásban
            // Másik fa bejárás:
            //      2 service kellene, egyik csak összeköti az embereket, a másik csak kiírja
            //      kétszer futna le a fa bejárás
            if (node == null || node.Person == null) return;

            node.Mother = new Business.Node(node.Person.Mother);
            node.Mother.LeftMostChild = node;
            node.Mother.TopCoordinate = node.TopCoordinate - (node.Height + 25);
            node.Mother.LeftCoordinate = node.LeftCoordinate - (node.Width + 25) * 0.5;
            PostOrder(node.Mother);

            node.Father = new Business.Node(node.Person.Father);
            node.Father.LeftMostChild = node;
            node.Father.TopCoordinate = node.TopCoordinate - (node.Height + 25);
            node.Father.LeftCoordinate = node.LeftCoordinate + (node.Width + 25) * 0.5;
            PostOrder(node.Father);

            Visit(node);

            if (node.Mother.Person == null && node.Father.Person == null)
            {
                node.LeftCoordinate = _leftmostValue;
                _leftmostValue += node.Width + 25;
            }
            else if (node.Mother.Person == null)
            {
                node.LeftCoordinate = node.Father.LeftCoordinate;
                CreateArc(node.Mother, node);
            }
            else if (node.Father.Person == null)
            {
                node.LeftCoordinate = node.Mother.LeftCoordinate;
                CreateArc(node.Father, node);
            }
            else
            {
                node.LeftCoordinate = (node.Mother.LeftCoordinate + node.Father.LeftCoordinate) * 0.5;
                CreateArc(node.Mother, node.Father, node);
            }
        }

        public void PreOrder(Business.Node node)
        {
            throw new NotImplementedException();
        }

        public void CreateArc(Business.Point from, Business.Point to)
        {
            var line = new Line();

            line.TopCoordinate = from.Y;
            line.LeftCoordinate = from.X;
            line.BottomCoordinate = to.Y;
            line.RigthCoordinate = to.X;

            Lines.Add(line);
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

        public void Visit(Business.Node node)
        {
            Nodes.Add(node);
        }
    }
}
