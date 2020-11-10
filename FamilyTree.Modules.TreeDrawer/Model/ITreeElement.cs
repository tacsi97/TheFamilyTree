using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTree.Modules.TreeDrawer.Model
{
    public interface ITreeElement
    {
        public double LeftCoordinate { get; set; }

        public double TopCoordinate { get; set; }

        public double RigthCoordinate { get; set; }

        public double BottomCoordinate { get; set; }
    }
}
