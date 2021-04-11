namespace FamilyTree.Services.ArcDrawer.Interfaces
{
    public interface IArcDrawer<T>
    {
        void CreateArc(T from, T to);
    }
}
