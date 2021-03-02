﻿using FamilyTree.Core;
using FamilyTree.Core.Attributes;
using System.Windows.Controls;

namespace FamilyTree.Modules.Relationship.Views
{
    /// <summary>
    /// Interaction logic for EditRelationshipView
    /// </summary>
    
    [DependentView(RegionNames.ContentRegion, typeof(BackRelationshipView))]
    public partial class EditRelationshipView : UserControl
    {
        public EditRelationshipView()
        {
            InitializeComponent();
        }
    }
}
