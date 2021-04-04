using FamilyTree.Business;
using FamilyTree.Modules.TreeDrawer.Adapters;

namespace FamilyTree.Modules.TreeDrawer.Model
{
    public class LineTreeElement : TreeElementAdapter<Line>
    {

        #region Properties

        public override double LeftCoordinate
        {
            get => TObject.LeftCoordinate;
            set => TObject.LeftCoordinate = value;
        }

        public override double TopCoordinate
        {
            get => TObject.TopCoordinate;
            set => TObject.TopCoordinate = value;
        }

        public override double RigthCoordinate
        {
            get => TObject.RigthCoordinate;
            set => TObject.RigthCoordinate = value;
        }

        public override double BottomCoordinate
        {
            get => TObject.BottomCoordinate;
            set => TObject.BottomCoordinate = value;
        }

        #endregion

        public LineTreeElement(Line tObject) : base(tObject)
        {
        }
    }
}
