namespace FamilyTree.Services.TreeTravelsal.Interfaces
{
    public interface ITreeTraversal<T>
    {
        void Traverse(T person);

        void Visit(T person);
    }
}
