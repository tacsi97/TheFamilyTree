using FamilyTree.Services.TreeTravelsal.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTree.Services.TreeTravelsal
{
    public class TreeTravelsalText : ITreeTravelsalText<Business.Node>
    {
        #region Properties

        public StringBuilder Builder { get; set; }

        public Business.Node Root { get; set; }

        public Business.Person SelectedPerson { get; set; }

        #endregion

        public TreeTravelsalText()
        {
            Builder = new StringBuilder();
        }

        public void PostOrder(Business.Node node)
        {
            // TODO: Vagy itt kötögetem össze az embereket, vagy egy másik fa bejárásban
            // Másik fa bejárás:
            //      2 service kellene, egyik csak összeköti az embereket, a másik csak kiírja
            //      kétszer futna le a fa bejárás
            if (node == null || node.Person == null) return;

            node.Mother = new Business.Node(node.Person.Mother);
            node.Mother.LeftMostChild = node;
            node.Mother.TopCoordinate = node.TopCoordinate - (node.Height + 25);
            node.Mother.LeftCoordinate = node.LeftCoordinate - (node.Width + 25)*0.5;
            PostOrder(node.Mother);

            node.Father = new Business.Node(node.Person.Father);
            node.Father.LeftMostChild = node;
            node.Father.TopCoordinate = node.TopCoordinate - (node.Height + 25);
            node.Father.LeftCoordinate = node.LeftCoordinate + (node.Width + 25)*0.5;
            PostOrder(node.Father);

            Visit(node);
        }

        public void PreOrder(Business.Node node)
        {
            throw new NotImplementedException();
        }

        public void Visit(Business.Node node)
        {
            Builder.Append(node.Person.FirstName);
            Builder.Append(" ");
            Builder.Append(node.Person.LastName);
            Builder.Append(", ");
        }
    }
}
