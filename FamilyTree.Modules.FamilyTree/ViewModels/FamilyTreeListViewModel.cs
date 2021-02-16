using FamilyTree.Core;
using FamilyTree.Modules.FamilyTree.Commands;
using FamilyTree.Modules.FamilyTree.PubSubEvents;
using FamilyTree.Services.Repository.Interfaces;
using Prism.Commands;
using Prism.Events;
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
        private readonly IEventAggregator _eventAggregator;

        #endregion

        #region Properties

        public ObservableCollection<Business.FamilyTree> Trees { get; set; }

        private Business.FamilyTree _familyTree;
        public Business.FamilyTree FamilyTree
        {
            get { return _familyTree; }
            set
            {
                SetProperty(ref _familyTree, value);
                _eventAggregator.GetEvent<SelectedTreeChanged>().Publish(_familyTree);
            }
        }

        #endregion

        #region Commands

        public GetTreesCommand GetTreesCommand { get; set; }

        #endregion

        public FamilyTreeListViewModel(IAsyncRepository<Business.FamilyTree> repository, IEventAggregator eventAggregator)
        {
            _repository = repository;
            _eventAggregator = eventAggregator;

            Trees = new ObservableCollection<Business.FamilyTree>();

            GetTreesCommand = new GetTreesCommand(this);
        }

        public async Task ExecuteGetTreesCommand()
        {
            Trees.Clear();

            var trees = await _repository.GetAllAsync();

            foreach (var tree in trees)
            {
                Trees.Add(tree);
            }
        }
    }
}
