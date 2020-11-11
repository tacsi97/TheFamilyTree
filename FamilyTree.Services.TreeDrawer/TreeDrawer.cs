using FamilyTree.Business;
using FamilyTree.Modules.Relationship.Core;
using FamilyTree.Services.TreeDrawer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FamilyTree.Services.TreeDrawer
{
    public class TreeDrawer : ITreeDrawer
    {
        public ICollection<Node> Nodes { get; set; }

        public double HorizontalSpace { get => 20d; }

        public double VerticalSpace { get => 20d; }

        public TreeDrawer()
        {
            Nodes = new List<Node>();
        }

        public ICollection<Node> ArrangeUpperTree(Node node)
        {
            var checkedNodes = new List<Node>();

            var stack = new Stack<Node>();
            var minX = 0d;
            while (node != null && (GetUncheckedParentNode(node) != null || GetUncheckedPairNode(node) != null))
            {
                if (GetUncheckedParentNode(node) != null)
                {
                    stack.Push(node);
                    node = GetUncheckedParentNode(node);
                }

                while (GetUncheckedParentNode(node) == null
                    && !(stack.Count == 0 && GetUncheckedPairNode(node) == null))
                {
                    SetParentPosition(node, minX);
                    checkedNodes.Add(node);
                    if (minX <= node.LeftCoordinate)
                        minX = node.LeftCoordinate + node.Width + HorizontalSpace;
                    if (GetUncheckedParentNode(node) == null)
                        if (GetUncheckedPairNode(node) != null)
                            node = GetUncheckedPairNode(node);
                        else
                            node = stack.Pop();
                    else
                        node = stack.Peek();
                }
            }
            if (node != null && !node.IsChecked)
            {
                SetParentPosition(node, minX);
                checkedNodes.Add(node);
            }
            foreach (var personNode in Nodes)
            {
                personNode.IsChecked = false;
            }

            return checkedNodes;
        }

        public ICollection<Node> ArrangeLowerTree(Node node)
        {
            var checkedNodes = new List<Node>();

            var stack = new Stack<Node>();
            var minX = 0d;
            while (node != null && GetUncheckedChildNode(node) != null)
            {
                stack.Push(node);
                node = GetUncheckedChildNode(node);
                while (GetUncheckedChildNode(node) == null && stack.Count != 0)
                {
                    checkedNodes.Add(node);
                    if (node.Person.Children.Count != 0)
                    {
                        if (node.Person.Partners.First().Equals(node.Person))
                        {
                            var leftMostPerson = node.Person;
                            while (true)
                            {
                                if (leftMostPerson.Children.FirstOrDefault() != null)
                                    leftMostPerson = leftMostPerson.Children.FirstOrDefault();
                                else
                                    break;
                            }
                            node.LeftCoordinate = GetNode(leftMostPerson).LeftCoordinate;
                        }
                        else
                        {
                            // előző algo miatt
                            var members = new List<Business.Person>()
                            {
                                node.Person
                            };
                            node.Person.Partners.ToList().ForEach((relation) =>
                            {
                                if (relation.RelationType.Equals(TypeNames.Partner))
                                    members.Add(relation.PersonTo);
                            });

                            var index = members.IndexOf(node.Person);

                            node.LeftCoordinate = GetNode(members[index - 1]).LeftCoordinate + node.Width + HorizontalSpace;
                        }
                    }
                    else
                        node.LeftCoordinate = minX;
                    node.IsChecked = true;
                    if (minX <= node.LeftCoordinate)
                        minX = node.LeftCoordinate + node.Width + HorizontalSpace;
                    if (stack.Count != 0)
                        if (GetUncheckedChildNode(node) == null)
                            if (GetUncheckedPairNode(node) == null)
                                node = stack.Pop();
                            else
                                node = GetUncheckedPairNode(node);
                        else
                            node = stack.Peek();
                }
            }
            foreach (var personNode in checkedNodes)
            {
                personNode.IsChecked = false;
            }

            return checkedNodes;
        }

        public Node GetUncheckedParentNode(Node node)
        {
            var fatherNode = GetNode(node.Person.Father);
            var motherNode = GetNode(node.Person.Mother);

            if (node.Person.Father != null
                && fatherNode != null
                && !fatherNode.IsChecked)
                return fatherNode;
            else if (node.Person.Mother != null
                && motherNode != null
                && !motherNode.IsChecked)
                return motherNode;
            else
                return null;
        }

        public void SetParentPosition(Node node, double minX)
        {
            var fatherNode = GetNode(node.Person.Father);
            var motherNode = GetNode(node.Person.Mother);

            var parentCount = 0;

            if (fatherNode != null) parentCount++;
            if (motherNode != null) parentCount++;

            if (parentCount == 0)
            {
                node.LeftCoordinate = minX;
            }
            else
            {
                var sum = 0d;
                if (node.Person.Mother != null)
                    sum += motherNode.LeftCoordinate;
                if (node.Person.Father != null)
                    sum += fatherNode.LeftCoordinate;
                node.LeftCoordinate = sum / parentCount;
            }
            node.IsChecked = true;
        }

        public Node GetUncheckedPairNode(Node node)
        {
            Node pairNode = null;

            foreach (var relation in node.Person.Partners)
            {
                var iterationNode = GetNode(relation.PersonTo);
                if (!iterationNode.IsChecked)
                {
                    pairNode = iterationNode;
                    break;
                }
            }

            return pairNode;
        }

        public Node GetUncheckedChildNode(Node node)
        {
            Node uncheckedChildNode = null;

            foreach (var child in node.Person.Children)
            {
                var iterationNode = GetNode(child);
                if (!iterationNode.IsChecked)
                {
                    uncheckedChildNode = iterationNode;
                    break;
                }
            }

            return uncheckedChildNode;
        }

        public Node GetNode(Business.Person person)
        {
            return Nodes.FirstOrDefault((node) => node.Person == person);
        }

        public ICollection<Line> Createlines()
        {
            var lines = new List<Line>();

            foreach (var node in Nodes)
            {
                foreach (var innerNode in Nodes)
                {
                    Line line;
                    if (!innerNode.Equals(node))
                    {
                        if (node.Person.Father != null && node.Person.Father.Equals(innerNode.Person))
                        {
                            if (node.LeftCoordinate == innerNode.LeftCoordinate)
                            {
                                line = new Line()
                                {
                                    LeftCoordinate = node.LeftCoordinate + node.Width / 2,
                                    TopCoordinate = node.TopCoordinate,
                                    RigthCoordinate = innerNode.LeftCoordinate + innerNode.Width / 2,
                                    BottomCoordinate = innerNode.BottomCoordinate
                                };
                                lines.Add(line);
                            }
                            else
                            {
                                var upperSide = new Line()
                                {
                                    LeftCoordinate = innerNode.LeftCoordinate + innerNode.Width / 2,
                                    TopCoordinate = innerNode.BottomCoordinate,
                                    RigthCoordinate = innerNode.LeftCoordinate + innerNode.Width / 2,
                                    BottomCoordinate = innerNode.BottomCoordinate + VerticalSpace / 2
                                };
                                var middle = new Line()
                                {
                                    LeftCoordinate = innerNode.LeftCoordinate + innerNode.Width / 2,
                                    TopCoordinate = innerNode.BottomCoordinate + VerticalSpace / 2,
                                    RigthCoordinate = node.LeftCoordinate + node.Width / 2,
                                    BottomCoordinate = innerNode.BottomCoordinate + VerticalSpace / 2
                                };
                                var lowerSide = new Line()
                                {
                                    LeftCoordinate = node.LeftCoordinate + node.Width / 2,
                                    TopCoordinate = node.TopCoordinate,
                                    RigthCoordinate = node.LeftCoordinate + node.Width / 2,
                                    BottomCoordinate = node.TopCoordinate - VerticalSpace / 2
                                };
                                lines.Add(upperSide);
                                lines.Add(middle);
                                lines.Add(lowerSide);
                            }
                        }
                        else if (node.Person.Mother != null && node.Person.Mother.Equals(innerNode.Person))
                        {
                            if (node.LeftCoordinate == innerNode.LeftCoordinate)
                            {
                                line = new Line()
                                {
                                    LeftCoordinate = node.LeftCoordinate + node.Width / 2,
                                    TopCoordinate = node.TopCoordinate,
                                    RigthCoordinate = innerNode.LeftCoordinate + innerNode.Width / 2,
                                    BottomCoordinate = innerNode.BottomCoordinate
                                };
                                lines.Add(line);
                            }
                            else
                            {
                                var upperSide = new Line()
                                {
                                    LeftCoordinate = innerNode.LeftCoordinate + innerNode.Width / 2,
                                    TopCoordinate = innerNode.BottomCoordinate,
                                    RigthCoordinate = innerNode.LeftCoordinate + innerNode.Width / 2,
                                    BottomCoordinate = innerNode.BottomCoordinate + VerticalSpace / 2
                                };
                                var middle = new Line()
                                {
                                    LeftCoordinate = innerNode.LeftCoordinate + innerNode.Width / 2,
                                    TopCoordinate = innerNode.BottomCoordinate + VerticalSpace / 2,
                                    RigthCoordinate = node.LeftCoordinate + node.Width / 2,
                                    BottomCoordinate = innerNode.BottomCoordinate + VerticalSpace / 2
                                };
                                var lowerSide = new Line()
                                {
                                    LeftCoordinate = node.LeftCoordinate + node.Width / 2,
                                    TopCoordinate = node.TopCoordinate,
                                    RigthCoordinate = node.LeftCoordinate + node.Width / 2,
                                    BottomCoordinate = node.TopCoordinate - VerticalSpace / 2
                                };
                                lines.Add(upperSide);
                                lines.Add(middle);
                                lines.Add(lowerSide);
                            }
                        }
                        else if (node.Person.Partners.ToList().Exists((relationship) =>
                            (relationship.PersonFrom.Equals(innerNode.Person)
                            || relationship.PersonTo.Equals(innerNode.Person))
                        ))
                        {
                            var between = Nodes.Where(
                                    member => (member.TopCoordinate == innerNode.TopCoordinate
                                    && ((node.LeftCoordinate < member.LeftCoordinate && member.LeftCoordinate < innerNode.LeftCoordinate)
                                    || (innerNode.LeftCoordinate < member.LeftCoordinate && member.LeftCoordinate < node.LeftCoordinate))
                                    && !member.Equals(node) && !member.Equals(innerNode))).FirstOrDefault();
                            if (between == null)
                                //megkeresi a rövidebb utat [ ]-[ ], és nem így köti össze [-]-[-]
                                if (Math.Abs(node.LeftCoordinate - innerNode.RigthCoordinate) <
                                    Math.Abs(innerNode.LeftCoordinate - node.RigthCoordinate))
                                {
                                    line = new Line()
                                    {
                                        LeftCoordinate = node.LeftCoordinate,
                                        TopCoordinate = node.TopCoordinate + node.Height / 2,
                                        RigthCoordinate = innerNode.RigthCoordinate,
                                        BottomCoordinate = innerNode.TopCoordinate + innerNode.Height / 2
                                    };
                                    lines.Add(line);
                                }
                                else
                                {
                                    line = new Line()
                                    {
                                        LeftCoordinate = innerNode.LeftCoordinate,
                                        TopCoordinate = innerNode.TopCoordinate + node.Height / 2,
                                        RigthCoordinate = node.RigthCoordinate,
                                        BottomCoordinate = innerNode.TopCoordinate + innerNode.Height / 2
                                    };
                                    lines.Add(line);
                                };
                        }
                    }
                }
            }
            return lines;
        }

        public ICollection<Node> GetNodes()
        {
            return Nodes;
        }

        public void SetNodes(ICollection<Node> nodes)
        {
            Nodes = nodes;
        }
    }
}
