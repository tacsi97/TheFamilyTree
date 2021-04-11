using FamilyTree.Modules.TreeDrawer.Model;

namespace FamilyTree.Modules.TreeDrawer.Adapters
{
    public abstract class TreeElementAdapter<T> : ITreeElement
    {
        public T TObject { get; set; }

        public abstract double LeftCoordinate { get; set; }

        public abstract double TopCoordinate { get; set; }

        public abstract double RigthCoordinate { get; set; }

        public abstract double BottomCoordinate { get; set; }

        public TreeElementAdapter(T tObject)
        {
            TObject = tObject;
        }
    }
}
