namespace FamilyTree.Business
{
    public class Node : ITreeElement
    {
        public Person Person { get; set; }

        public bool IsChecked { get; set; }

        public double LeftCoordinate { get; set; }

        public double TopCoordinate { get; set; }

        public double RigthCoordinate { get => LeftCoordinate + Width; set { } }

        public double BottomCoordinate { get => TopCoordinate + Height; set { } }

        public double Width { get; set; }

        public double Height { get; set; }

        public Node(Person person)
        {
            Person = person;
            if (person != null)
                person.Node = this;
            Width = 150;
            Height = 200;
            TopCoordinate = 0;
            LeftCoordinate = 0;
        }

        public Node(Person person, double width, double height)
        {
            Person = person;
            Width = width;
            Height = height;
            TopCoordinate = 0;
            LeftCoordinate = 0;
        }
    }
}
