namespace FamilyTree.Services.TreeTravelsal.Interfaces
{
    public interface ITreeTraversal<T>
    {
        void PostOrder(T person);

        void PreOrder(T person);

        void Visit(T person);
    }
}
