using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTree.Business
{
    public class BusinessBase : BindableBase
    {
        private int _id;
        public int ID
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
    }
}
