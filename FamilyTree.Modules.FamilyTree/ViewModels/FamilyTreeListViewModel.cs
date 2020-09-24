using FamilyTree.Core;
using FamilyTree.Modules.FamilyTree.Commands;
using FamilyTree.Services.Repository.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.Modules.FamilyTree.ViewModels
{
    public class FamilyTreeListViewModel : BindableBase
    {
        #region Fields

        private readonly IAsyncRepository<Business.FamilyTree> _repository;

        #endregion

        #region Properties

        public ObservableCollection<Business.FamilyTree> Trees { get; set; }

        #endregion

        #region Commands

        public GetTreesCommand GetTreesCommand { get; set; }

        #endregion

        public FamilyTreeListViewModel(IAsyncRepository<Business.FamilyTree> repository)
        {
            _repository = repository;

            Trees = new ObservableCollection<Business.FamilyTree>();

            GetTreesCommand = new GetTreesCommand(this);
        }

        public async Task ExecuteGetTreesCommand()
        {
            Trees.Clear();

            var trees = await _repository.GetAllAsync(Uris.FamilyTreeURI);

            foreach (var tree in trees)
            {
                Trees.Add(tree);
            }
        }
    }
}
