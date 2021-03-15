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

        public void PostOrder(Person person)
        {
            if (person == null) return;

            person.Node = new Node(person);

            person.Mother.Node = new Business.Node(person.Mother);
            person.Mother.LeftmostChild.Node = person.Node;
            person.Mother.Node.TopCoordinate = person.Node.TopCoordinate - (person.Node.Height + 25);
            person.Mother.Node.LeftCoordinate = person.Node.LeftCoordinate - (person.Node.Width + 25) * 0.5;
            PostOrder(person.Mother);

            person.Father.Node = new Business.Node(person.Father);
            person.Father.LeftmostChild.Node = person.Node;
            person.Father.Node.TopCoordinate = person.Node.TopCoordinate - (person.Node.Height + 25);
            person.Father.Node.LeftCoordinate = person.Node.LeftCoordinate + (person.Node.Width + 25) * 0.5;
            PostOrder(person.Father);

            Visit(person);
        }

        public void PreOrder(Person person)
        {
            throw new NotImplementedException();
        }

        public void Visit(Person person)
        {
            if (person.Mother == null && person.Father == null)
            {
                person.Node.LeftCoordinate = _leftmostValue;
                _leftmostValue += person.Node.Width + 25;
            }
            else if (person.Mother == null)
            {
                person.Node.LeftCoordinate = person.Father.Node.LeftCoordinate;
                CreateArc(person.Mother.Node, person.Node);
            }
            else if (person.Father == null)
            {
                person.Node.LeftCoordinate = person.Mother.Node.LeftCoordinate;
                CreateArc(person.Father.Node, person.Node);
            }
            else
            {
                person.Node.LeftCoordinate = (person.Mother.Node.LeftCoordinate + person.Father.Node.LeftCoordinate) * 0.5;
                CreateArc(person.Mother.Node, person.Father.Node, person.Node);
            }

            Nodes.Add(person.Node);
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
    }
}
