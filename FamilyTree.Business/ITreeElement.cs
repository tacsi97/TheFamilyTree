﻿namespace FamilyTree.Business
{
    public interface ITreeElement
    {
        double LeftCoordinate { get; set; }

        double TopCoordinate { get; set; }

        double RigthCoordinate { get; set; }

        double BottomCoordinate { get; set; }
    }
}
