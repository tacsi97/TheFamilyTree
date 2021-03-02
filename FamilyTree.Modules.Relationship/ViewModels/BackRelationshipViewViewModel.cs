using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FamilyTree.Modules.Relationship.ViewModels
{
    public class BackRelationshipViewViewModel : BindableBase
    {
        #region Fields

        private readonly IRegionManager _regionManager;

        #endregion

        #region Commands



        #endregion

        public BackRelationshipViewViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
    }
}
