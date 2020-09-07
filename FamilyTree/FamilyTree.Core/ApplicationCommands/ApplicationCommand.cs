using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTree.Core.ApplicationCommands
{
    public class ApplicationCommand : IApplicationCommand
    {
        private CompositeCommand _navigateCommand;

        public CompositeCommand NavigateCommand { get; } = new CompositeCommand();
    }
}
