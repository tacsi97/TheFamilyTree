using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTree.Core.ApplicationCommands
{
    public class ApplicationCommand : IApplicationCommand
    {
        public CompositeCommand NavigateCommand { get; } = new CompositeCommand();
    }
}
