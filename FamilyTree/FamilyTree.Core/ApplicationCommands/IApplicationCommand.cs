using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTree.Core.ApplicationCommands
{
    public interface IApplicationCommand
    {
        CompositeCommand NavigateCommand { get; }
    }
}
