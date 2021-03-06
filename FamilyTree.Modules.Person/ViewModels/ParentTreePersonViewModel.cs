using FamilyTree.Services.Repository.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FamilyTree.Modules.Person.ViewModels
{
    public class ParentTreePersonViewModel : BindableBase
    {
        #region Fields

        private readonly IAsyncRepository<Business.Person> _repository;

        #endregion

        #region Properties

        public Business.Person SelectedPerson { get; set; }

        #endregion

        #region Commands

        private DelegateCommand _drawCommand;
        public DelegateCommand DrawCommand =>
            _drawCommand ?? (_drawCommand = new DelegateCommand(ExecuteDrawCommand));

        #endregion

        public ParentTreePersonViewModel(IAsyncRepository<Business.Person> repository)
        {
            _repository = repository;
        }

        public void ExecuteDrawCommand()
        {

        }

    }
}
