using FamilyTree.Core;
using FamilyTree.Modules.FamilyTree.Commands;
using FamilyTree.Modules.FamilyTree.Core;
using FamilyTree.Services.Repository.Interfaces;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FamilyTree.Modules.FamilyTree.ViewModels
{
    public class CreateFamilyTreeViewModel : BindableBase, INavigationAware
    {
        private readonly IAsyncRepository<Business.FamilyTree> _repository;
        private readonly IRegionManager _regionManager;

        public Business.FamilyTree FamilyTree { get; set; }

        private string _familyTreeName;
        public string FamilyTreeName
        {
            get { return _familyTreeName; }
            set
            {
                SetProperty(ref _familyTreeName, value);
                SubmitCommand.RaiseCanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public SubmitCommand SubmitCommand { get; set; }

        public CreateFamilyTreeViewModel(IAsyncRepository<Business.FamilyTree> repository, IRegionManager regionManager)
        {
            _repository = repository;
            _regionManager = regionManager;

            SubmitCommand = new SubmitCommand(this);
        }

        /// <summary>
        /// Determines that, the command can be executed or not.
        /// </summary>
        /// <returns>If the <c>NewTreeDialogViewModel</c>'s FamilyTreeName property is null or empty, then it return false.</returns>
        public bool SubmitCanExecute() => !string.IsNullOrEmpty(FamilyTreeName);

        public async Task SubmitExecute()
        {
            FamilyTree = new Business.FamilyTree()
            {
                Name = FamilyTreeName,
                People = new ObservableCollection<Business.Person>()
            };

            var createdTree = await _repository.CreateAsync(FamilyTree);

            var navParams = new NavigationParameters();
            navParams.Add("FamilyTree", createdTree);

            _regionManager.RequestNavigate(RegionNames.ContentRegion, "ListFamilyTreeView", navParams);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            return;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            navigationContext.Parameters.Add(NavParamNames.Tree, FamilyTree);
        }
    }
}
