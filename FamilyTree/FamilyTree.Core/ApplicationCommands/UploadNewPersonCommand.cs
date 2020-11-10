using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTree.Core.ApplicationCommands
{
    public class UploadNewPersonCommand : IUpload
    {
        public CompositeCommand CompositeCommand { get; } = new CompositeCommand();
    }
}
