using FamilyTree.Business;
using FamilyTree.Services.Repository;
using FamilyTree.Services.Repository.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Modules.FamilyTree.Repository
{
    public class FakeTreeRepository : FakeRepositoryBase<Business.FamilyTree>
    {
        public FakeTreeRepository(string uri, Token token) : base(uri, token)
        {
            Collection.Add(new Business.FamilyTree() { ID = 0, Name = "Tóth Család", People = new ObservableCollection<Person>() });
            Collection.Add(new Business.FamilyTree() { ID = 1, Name = "Fodor Család", People = new ObservableCollection<Person>() });
            Collection.Add(new Business.FamilyTree() { ID = 2, Name = "Láng Család", People = new ObservableCollection<Person>() });
        }
    }
}
