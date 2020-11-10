using FamilyTree.Modules.TreeDrawer.Model;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FamilyTree.Services.TreeDrawer.Interfaces
{
    public interface ITreeDrawer
    {
        void Draw(ICollection<ITreeElement> treeElements);
    }
}
