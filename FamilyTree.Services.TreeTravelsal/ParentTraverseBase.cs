using FamilyTree.Business;
using FamilyTree.Services.TreeTravelsal.Interfaces;
using System;
using System.Collections.Generic;

namespace FamilyTree.Services.TreeTravelsal
{
    public abstract class ParentTraverseBase : ITreeTraversal<Business.Person>
    {
        #region Fields

        public double LeftmostValue = 0;

        #endregion

        #region Properties

        public ICollection<Business.Node> Nodes { get; set; }

        public ICollection<Business.Line> Lines { get; set; }

        #endregion

        public ParentTraverseBase()
        {
            Nodes = new List<Business.Node>();
            Lines = new List<Business.Line>();
        }

        public void Traverse(Person person)
        {
            if (person == null)
                return;

            person.Node = new Node(person);

            if (person.Mother != null)
                person.Mother.LeftmostChild = person;

            if (person.Father != null)
                person.Father.LeftmostChild = person;

            if (person.LeftmostChild != null && person.LeftmostChild.Node != null)
                person.Node.TopCoordinate = person.LeftmostChild.Node.TopCoordinate - (person.Node.Height + 25);

            Traverse(person.Mother);

            Traverse(person.Father);

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
                person.Node.LeftCoordinate = LeftmostValue;
                LeftmostValue += person.Node.Width + 25;
            }
            else if (person.Mother == null)
                person.Node.LeftCoordinate = person.Father.Node.LeftCoordinate;
            else if (person.Father == null)
                person.Node.LeftCoordinate = person.Mother.Node.LeftCoordinate;
            else
                person.Node.LeftCoordinate = (person.Mother.Node.LeftCoordinate + person.Father.Node.LeftCoordinate) * 0.5;

            // Vonalak
            // Csak egy szülő van megadva
            if (person.Father != null ^ person.Mother != null)
            {
                var notNullParent = (person.Father != null ? person.Father : person.Mother);

                // |
                Lines.Add(new Line()
                {
                    TopCoordinate = notNullParent.Node.BottomCoordinate,
                    LeftCoordinate = (notNullParent.Node.LeftCoordinate + notNullParent.Node.RigthCoordinate) * 0.5,
                    BottomCoordinate = person.Node.TopCoordinate,
                    RigthCoordinate = (notNullParent.Node.LeftCoordinate + notNullParent.Node.RigthCoordinate) * 0.5
                });
            }

            // Két szülő van megadva
            if (person.Mother != null && person.Father != null)
            {
                // node|---|node
                Lines.Add(new Line()
                {
                    TopCoordinate = (person.Mother.Node.TopCoordinate + person.Mother.Node.BottomCoordinate) * 0.5,
                    LeftCoordinate = person.Mother.Node.RigthCoordinate,
                    BottomCoordinate = (person.Father.Node.TopCoordinate + person.Father.Node.BottomCoordinate) * 0.5,
                    RigthCoordinate = person.Father.Node.LeftCoordinate
                });

                // |
                Lines.Add(new Line()
                {
                    TopCoordinate = (person.Mother.Node.TopCoordinate + person.Mother.Node.BottomCoordinate) * 0.5,
                    LeftCoordinate = (person.Node.LeftCoordinate + person.Node.RigthCoordinate) * 0.5,
                    BottomCoordinate = person.Node.TopCoordinate,
                    RigthCoordinate = (person.Node.LeftCoordinate + person.Node.RigthCoordinate) * 0.5
                });
            }

            Nodes.Add(person.Node);
        }
    }
}
