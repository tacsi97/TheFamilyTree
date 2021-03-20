﻿using FamilyTree.Core;
using FamilyTree.Core.Attributes;
using System.Windows.Controls;

namespace FamilyTree.Modules.Person.Views
{
    /// <summary>
    /// Interaction logic for CreateChildView
    /// </summary>
    [DependentView(RegionNames.FunctionRegion, typeof(BackPersonView))]
    public partial class CreateChildView : UserControl
    {
        public CreateChildView()
        {
            InitializeComponent();
        }
    }
}
