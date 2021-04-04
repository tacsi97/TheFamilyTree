using Prism.Commands;

namespace FamilyTree.Core.ApplicationCommands
{
    public class ApplicationCommand : IApplicationCommand
    {
        public CompositeCommand CompositeCommand { get; } = new CompositeCommand();
    }
}
