using FamilyTree.Business;
using FamilyTree.Modules.Relationship.Core;
using FamilyTree.Services.PersonConnector.Interfaces;
using System;
using System.Collections.Generic;

namespace FamilyTree.Services.PersonConnector
{
    public class PersonConnector : IPersonConnector
    {
        public ICollection<Business.Person> ConnectPeople(IEnumerable<Business.Relationship> relationships)
        {
            // Összes relation-t lekérdezni, ami az adott fában van. Egy relation tartalmaz 2 embert, és a köztük lévő kapcsolatot is, ezekből pedig fel lehet építeni a fát.

            var result = new List<Person>();

            foreach (var relation in relationships)
            {
                if (relation.RelationType.Equals(TypeNames.Parent))
                {
                    // connect parents
                    if (relation.PersonFrom.Gender.Equals(GenderType.Male))
                        relation.PersonFrom.Father = relation.PersonTo;
                    else
                        relation.PersonFrom.Mother = relation.PersonTo;

                    relation.PersonTo.Children.Add(relation.PersonFrom);
                }
                else if (relation.RelationType.Equals(TypeNames.Child))
                {
                    // connect child
                    relation.PersonFrom.Children.Add(relation.PersonTo);

                    if (relation.PersonFrom.Gender.Equals(GenderType.Male))
                        relation.PersonTo.Father = relation.PersonFrom;
                    else
                        relation.PersonTo.Mother = relation.PersonFrom;
                }
                else if (relation.RelationType.Equals(TypeNames.Partner))
                {
                    // nem baj, ha a szülők és a gyerekek nem Relationship-ként vannak tárolva,
                    // mivel feltöltéskor Relationshipként megy fel,
                    // letöltéskor pedig szintén Relationshipként 
                    // módosításkor pedig lekérjük a Relationship objektumot
                    // connect partner
                    relation.PersonFrom.Partners.Add(new Business.Relationship()
                    {
                        RelationType = relation.RelationType,
                        From = relation.From,
                        To = relation.To,
                        PersonFrom = relation.PersonFrom,
                        PersonTo = relation.PersonTo
                    });

                    relation.PersonTo.Partners.Add(new Business.Relationship()
                    {
                        RelationType = relation.RelationType,
                        From = relation.From,
                        To = relation.To,
                        PersonFrom = relation.PersonTo,
                        PersonTo = relation.PersonFrom
                    });
                }

            }

            return result;
        }
    }
}
