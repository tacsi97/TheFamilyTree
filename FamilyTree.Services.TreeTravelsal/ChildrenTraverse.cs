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
        public ICollection<Node> Nodes { get; set; }
        public ICollection<Line> Lines { get; set; }

        public double LeftmostValue { get; set; }

        public ChildrenTraverse()
        {
            Nodes = new ObservableCollection<Node>();
            Lines = new ObservableCollection<Line>();
        }

        // TODO: Az emebereken iterálunk végig, így jó lenne ha az emberben lenne egy hivatkozás a node ra, ami tartalmazza.
        // mivel így a node-nak is kellenek azok az attribútumok, mint pl leftmostchildren, mother, father...
        public void PostOrder(Person person)
        {
            if (person == null) return;

            person.Node = new Node(person);

            if (person.Father != null && person.Father.Node != null)
                person.Node.TopCoordinate = person.Father.Node.BottomCoordinate + 25;
            else if (person.Mother != null && person.Mother.Node != null)
                person.Node.TopCoordinate = person.Mother.Node.BottomCoordinate + 25;
            else
                person.Node.TopCoordinate = 0;

            person.LeftmostChild = person.Children.FirstOrDefault();
            for (int i = 1; i < person.Children.Count; i++)
            {
                person.Children.ElementAt(i - 1).RightSibling = person.Children.ElementAt(i);
            }

            PostOrder(person.LeftmostChild);

            Visit(person);

            PostOrder(person.RightSibling);
        }

        public void PreOrder(Person person)
        {
            throw new NotImplementedException();
        }

        public void Visit(Person person)
        {
            if (person.LeftmostChild == null)
            {
                person.Node.LeftCoordinate = LeftmostValue;
                LeftmostValue += person.Node.Width + 25;
            }
            else
            {
                person.Node.LeftCoordinate = (person.Children.First().Node.LeftCoordinate + person.Children.Last().Node.LeftCoordinate) * 0.5;
                person.Node.TopCoordinate = person.LeftmostChild.Node.TopCoordinate - (person.Node.Height + 25);
            }

            if (person.Partner != null)
            {
                person.Partner.Node = new Node(person.Partner);

                person.Node.LeftCoordinate -= (person.Node.Width + 25) * 0.5;

                person.Partner.Node.LeftCoordinate = person.Node.RigthCoordinate + 25;
                person.Partner.Node.TopCoordinate = person.Node.TopCoordinate;

                LeftmostValue = Math.Max(LeftmostValue, person.Partner.Node.RigthCoordinate + 25);

                Nodes.Add(person.Partner.Node);
            }

            // Összekötni a párjával
            if (person.Partner != null)
                Lines.Add(new Line()
                {
                    TopCoordinate = (person.Node.TopCoordinate + person.Node.BottomCoordinate) * 0.5,
                    LeftCoordinate = person.Node.RigthCoordinate,
                    BottomCoordinate = (person.Node.TopCoordinate + person.Node.BottomCoordinate) * 0.5,
                    RigthCoordinate = person.Partner.Node.LeftCoordinate
                });

            // Összekötni az összes gyerekkel (lefelé vonal, vízszintes vonal, sok lefelé vonal)
            // lefelé
            if (person.Children.Count != 0)
            {
                if (person.Partner != null)
                    Lines.Add(new Line()
                    {
                        TopCoordinate = (person.Node.TopCoordinate + person.Node.BottomCoordinate) * 0.5,
                        LeftCoordinate = (person.Node.RigthCoordinate + person.Partner.Node.LeftCoordinate) * 0.5,
                        BottomCoordinate = person.Node.BottomCoordinate + (25 * 0.5),
                        RigthCoordinate = (person.Node.RigthCoordinate + person.Partner.Node.LeftCoordinate) * 0.5
                    });
                else
                    Lines.Add(new Line()
                    {
                        TopCoordinate = person.Node.BottomCoordinate + (25 * 0.5),
                        LeftCoordinate = (person.Node.LeftCoordinate + person.Node.RigthCoordinate) * 0.5,
                        BottomCoordinate = person.Node.BottomCoordinate + (25 * 0.5),
                        RigthCoordinate = (person.Node.LeftCoordinate + person.Node.RigthCoordinate) * 0.5
                    });
                // vízszintes
                if (person.Children.Count > 1)
                    Lines.Add(new Line()
                    {
                        TopCoordinate = person.Node.BottomCoordinate + (25 * 0.5),
                        LeftCoordinate = (person.Children.First().Node.LeftCoordinate + person.Children.First().Node.RigthCoordinate) * 0.5,
                        BottomCoordinate = person.Node.BottomCoordinate + (25 * 0.5),
                        RigthCoordinate = (person.Children.Last().Node.LeftCoordinate + person.Children.Last().Node.RigthCoordinate) * 0.5,
                    });
                // sok lefelé
                foreach (var child in person.Children)
                    Lines.Add(new Line()
                    {
                        TopCoordinate = child.Node.TopCoordinate - (25 * 0.5),
                        LeftCoordinate = (child.Node.LeftCoordinate + child.Node.RigthCoordinate) * 0.5,
                        BottomCoordinate = child.Node.TopCoordinate,
                        RigthCoordinate = (child.Node.LeftCoordinate + child.Node.RigthCoordinate) * 0.5
                    });
            }

            Nodes.Add(person.Node);
        }

        public void CreateArc(Business.Node upper, Business.Node lower)
        {
            var line = new Line();

            if(upper.Person.Partner == lower.Person)
            {
                // ---
                line.TopCoordinate = (upper.BottomCoordinate + upper.TopCoordinate) * 0.5;
                line.LeftCoordinate = upper.RigthCoordinate;
                line.BottomCoordinate = (lower.BottomCoordinate + lower.TopCoordinate) * 0.5;
                line.RigthCoordinate = lower.LeftCoordinate;
            }
            else
            {
                // |
                line.TopCoordinate = upper.BottomCoordinate;
                line.LeftCoordinate = (upper.LeftCoordinate + upper.RigthCoordinate) * 0.5;
                line.BottomCoordinate = lower.TopCoordinate;
                line.RigthCoordinate = (lower.LeftCoordinate + lower.RigthCoordinate) * 0.5;
            }

            Lines.Add(line);
        }

        public void CreateArc(Business.Node mother, Business.Node father, Business.Node child)
        {
            // node|---|node
            //Lines.Add(new Line()
            //{
            //    TopCoordinate = (mother.TopCoordinate + mother.BottomCoordinate) * 0.5,
            //    LeftCoordinate = mother.RigthCoordinate,
            //    BottomCoordinate = (father.TopCoordinate + father.BottomCoordinate) * 0.5,
            //    RigthCoordinate = father.LeftCoordinate
            //});

            Node notNullNode = (mother != null ? mother : father);

            // | 
            Lines.Add(new Line()
            {
                TopCoordinate = (notNullNode.TopCoordinate + notNullNode.BottomCoordinate) * 0.5,
                LeftCoordinate = (notNullNode.RigthCoordinate + 25) * 0.5,
                BottomCoordinate = notNullNode.BottomCoordinate + (25 * 0.5),
                RigthCoordinate = (notNullNode.RigthCoordinate + 25) * 0.5
            });

            // ---
            Lines.Add(new Line()
            {
                TopCoordinate = notNullNode.BottomCoordinate + (25 * 0.5),
                LeftCoordinate = (notNullNode.RigthCoordinate + 25) * 0.5,
                BottomCoordinate = notNullNode.BottomCoordinate + (25 * 0.5),
                RigthCoordinate = (child.LeftCoordinate + child.RigthCoordinate) * 0.5
            });

            // |
            Lines.Add(new Line()
            {
                TopCoordinate = notNullNode.BottomCoordinate + (25 * 0.5),
                LeftCoordinate = (child.LeftCoordinate + child.RigthCoordinate) * 0.5,
                BottomCoordinate = child.TopCoordinate,
                RigthCoordinate = (child.LeftCoordinate + child.RigthCoordinate) * 0.5
            });
        }
    }
}
