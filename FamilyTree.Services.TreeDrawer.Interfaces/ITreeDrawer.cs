using FamilyTree.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTree.Services.TreeDrawer.Interfaces
{
    public interface ITreeDrawer
    {
        double HorizontalSpace { get; }

        double VerticalSpace { get; }

        ICollection<Node> ArrangeUpperTree(Node node);

        ICollection<Node> ArrangeLowerTree(Node node);

        Node GetUncheckedParentNode(Node node);

        void SetParentPosition(Node node, double minX);

        Node GetUncheckedPairNode(Node node);

        Node GetUncheckedChildNode(Node node);

        Node GetNode(Business.Person person);

        ICollection<Line> Createlines();

        ICollection<Node> GetNodes();

        void SetNodes(ICollection<Node> nodes);
    }
}
