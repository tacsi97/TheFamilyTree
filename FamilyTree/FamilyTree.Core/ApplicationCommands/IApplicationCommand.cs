using Prism.Commands;

namespace FamilyTree.Core.ApplicationCommands
{
    public interface IApplicationCommand
    {
        CompositeCommand CompositeCommand { get; }
    }
}
