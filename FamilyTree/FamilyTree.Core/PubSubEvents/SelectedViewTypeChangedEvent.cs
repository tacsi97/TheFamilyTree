using FamilyTree.Business;
using Prism.Events;

namespace FamilyTree.Core.PubSubEvents
{
    public class SelectedViewTypeChangedEvent : PubSubEvent<ViewType>
    {
    }
}
