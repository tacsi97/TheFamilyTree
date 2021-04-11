using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace FamilyTree.Business
{
    public class FamilyTree : BusinessBase
    {
        private string _name;
        [JsonProperty("Name")]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private ObservableCollection<Person> _people = new ObservableCollection<Person>();
        [JsonIgnore]
        public ObservableCollection<Person> People
        {
            get { return _people; }
            set { SetProperty(ref _people, value); }
        }
    }
}
