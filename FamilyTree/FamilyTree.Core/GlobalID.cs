namespace FamilyTree.Core
{
    public static class GlobalID
    {
        private static int id = 0;

        public static int NewID()
        {
            return id++;
        }

        public static void SetID(int idParam)
        {
            id = idParam;
        }
    }
}
