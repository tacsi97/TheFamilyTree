using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTree.Business
{
    public class Point : BusinessBase
    {
        #region Properties

        public double X { get; set; }

        public double Y { get; set; }

        #endregion

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
