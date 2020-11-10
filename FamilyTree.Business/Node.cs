using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FamilyTree.Business
{
    public class Node
    {
        public Person Person { get; set; }

        public bool IsChecked { get; set; }

        public double LeftCoordinate { get; set; }

        public double TopCoordinate { get; set; }

        public double RigthCoordinate { get => LeftCoordinate + Width; set { } }

        public double BottomCoordinate { get => TopCoordinate + Height; set { } }

        public double Width { get; set; }

        public double Height { get; set; }

        public Node LeftMostChild { get; set; }

        public Node RightSibling { get; set; }

        public Node(Person person)
        {
            Person = person;
        }

        public Node(Person person, double width, double height)
        {
            Person = person;
            Width = width;
            Height = height;
        }
    }
}
