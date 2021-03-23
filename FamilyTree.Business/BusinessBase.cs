using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyTree.Business
{
    public class BusinessBase : BindableBase
    {
        // TODO: id-t átalakítani string-re, mivel a gráf adatbázis azt fogja használni
        private int _id;
        [JsonProperty("ID")]
        public int ID
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
    }
}
