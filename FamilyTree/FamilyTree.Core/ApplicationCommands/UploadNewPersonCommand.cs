using Prism.Commands;

namespace FamilyTree.Core.ApplicationCommands
{
    public class UploadNewPersonCommand : IUpload
    {
        public CompositeCommand CompositeCommand { get; } = new CompositeCommand();
    }
}
