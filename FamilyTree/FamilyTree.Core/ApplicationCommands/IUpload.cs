using Prism.Commands;

namespace FamilyTree.Core.ApplicationCommands
{
    public interface IUpload
    {
        CompositeCommand CompositeCommand { get; }
    }
}
