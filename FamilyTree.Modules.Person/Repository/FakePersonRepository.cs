using FamilyTree.Core.Extensions;
using FamilyTree.Services.Repository;
using FamilyTree.Services.Repository.Interfaces;
using Newtonsoft.Json;
using Prism.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FamilyTree.Modules.Person.Repository
{
    public class FakePersonRepository : LocalRepositoryBase<Business.Person>
    {
        public ObservableCollection<Business.Person> People = new ObservableCollection<Business.Person>();

        public int id = 0;

        public FakePersonRepository(string uri, Business.Token token)
            : base(uri, token)
        {
            People.Add(new Business.Person() { ID = 1, FirstName = "Taksony" });
            People.Add(new Business.Person() { ID = 2, FirstName = "??" });
            People.Add(new Business.Person() { ID = 3, FirstName = "Mihály" });
            People.Add(new Business.Person() { ID = 4, FirstName = "Géza" });
            People.Add(new Business.Person() { ID = 5, FirstName = "László", LastName = "Szár" });
            People.Add(new Business.Person() { ID = 6, FirstName = "Vazul" });
            People.Add(new Business.Person() { ID = 7, FirstName = "??" });
            People.Add(new Business.Person() { ID = 8, FirstName = "István" });
            People.Add(new Business.Person() { ID = 9, FirstName = "Judit" });
            People.Add(new Business.Person() { ID = 10, FirstName = "Ilona" });
            People.Add(new Business.Person() { ID = 11, FirstName = "Sarolt" });
            People.Add(new Business.Person() { ID = 12, FirstName = "Sarolt" });
            People.Add(new Business.Person() { ID = 13, FirstName = "Vologymirivna", LastName = "Premiszláva" });
            People.Add(new Business.Person() { ID = 14, FirstName = "Domoszláv" });
            People.Add(new Business.Person() { ID = 15, FirstName = "Katun of Bulgária" });
            People.Add(new Business.Person() { ID = 16, FirstName = "I. András" });
            People.Add(new Business.Person() { ID = 17, FirstName = "I. Béla" });
            People.Add(new Business.Person() { ID = 18, FirstName = "Levente" });
            People.Add(new Business.Person() { ID = 19, FirstName = "I. Boleszláv" });
            People.Add(new Business.Person() { ID = 20, FirstName = "Veszprém" });
            People.Add(new Business.Person() { ID = 21, FirstName = "Ottó"});
            People.Add(new Business.Person() { ID = 22, FirstName = "Imre" });
            People.Add(new Business.Person() { ID = 23, FirstName = "Gizella", LastName = "Liudolf" });
            People.Add(new Business.Person() { ID = 24, FirstName = "Piast", LastName = "Richeza" });
            People.Add(new Business.Person() { ID = 25, FirstName = "I. Géza" });
            People.Add(new Business.Person() { ID = 26, FirstName = "I. László" });
            People.Add(new Business.Person() { ID = 27, FirstName = "Lampert" });
            People.Add(new Business.Person() { ID = 28, FirstName = "Zsófia" });
            People.Add(new Business.Person() { ID = 29, FirstName = "Eufémia" });
            People.Add(new Business.Person() { ID = 30, FirstName = "Ilona" });

            // apa
            // Taksony - Mihály, Géza
            AddFather(People.First(person => person.ID == 3), People.First(person => person.ID == 1));
            AddFather(People.First(person => person.ID == 4), People.First(person => person.ID == 1));
            // Mihály - Szár László, Vazul
            AddFather(People.First(person => person.ID == 5), People.First(person => person.ID == 3));
            AddFather(People.First(person => person.ID == 6), People.First(person => person.ID == 3));
            // Géza - István, Ilona, Judit, Sarolt
            AddFather(People.First(person => person.ID == 8), People.First(person => person.ID == 4));
            AddFather(People.First(person => person.ID == 9), People.First(person => person.ID == 4));
            AddFather(People.First(person => person.ID == 10), People.First(person => person.ID == 4));
            AddFather(People.First(person => person.ID == 11), People.First(person => person.ID == 4));
            // Szár László - Domoszláv
            AddFather(People.First(person => person.ID == 14), People.First(person => person.ID == 5));
            // Vazul - András, Béla, Levente
            AddFather(People.First(person => person.ID == 16), People.First(person => person.ID == 6));
            AddFather(People.First(person => person.ID == 17), People.First(person => person.ID == 6));
            AddFather(People.First(person => person.ID == 18), People.First(person => person.ID == 6));
            // Boleszláv - Veszprém
            AddFather(People.First(person => person.ID == 20), People.First(person => person.ID == 19));
            // Géza - Ottó, Imre
            AddFather(People.First(person => person.ID == 21), People.First(person => person.ID == 25));
            AddFather(People.First(person => person.ID == 22), People.First(person => person.ID == 25));
            // Béla - Géza, László, Lampert, Zsófia, Eufémia, Ilona
            AddFather(People.First(person => person.ID == 25), People.First(person => person.ID == 17));
            AddFather(People.First(person => person.ID == 26), People.First(person => person.ID == 17));
            AddFather(People.First(person => person.ID == 27), People.First(person => person.ID == 17));
            AddFather(People.First(person => person.ID == 28), People.First(person => person.ID == 17));
            AddFather(People.First(person => person.ID == 29), People.First(person => person.ID == 17));
            AddFather(People.First(person => person.ID == 30), People.First(person => person.ID == 17));
            // anya
            // ?? - Mihály, Géza
            AddMother(People.First(person => person.ID == 3), People.First(person => person.ID == 2));
            AddMother(People.First(person => person.ID == 4), People.First(person => person.ID == 2));
            // ?? - Szár László, Vazul
            AddMother(People.First(person => person.ID == 5), People.First(person => person.ID == 7));
            AddMother(People.First(person => person.ID == 6), People.First(person => person.ID == 7));
            // Sarolt - István, Ilona, Judit, Sarolt
            AddMother(People.First(person => person.ID == 8), People.First(person => person.ID == 12));
            AddMother(People.First(person => person.ID == 9), People.First(person => person.ID == 12));
            AddMother(People.First(person => person.ID == 10), People.First(person => person.ID == 12));
            AddMother(People.First(person => person.ID == 11), People.First(person => person.ID == 12));
            // Premiszláva - Domoszláv
            AddMother(People.First(person => person.ID == 14), People.First(person => person.ID == 13));
            // Katun - András, Béla, Levente
            AddMother(People.First(person => person.ID == 16), People.First(person => person.ID == 15));
            AddMother(People.First(person => person.ID == 17), People.First(person => person.ID == 15));
            AddMother(People.First(person => person.ID == 18), People.First(person => person.ID == 15));
            // Judit - Veszprém
            AddMother(People.First(person => person.ID == 20), People.First(person => person.ID == 9));
            // Gizella - Ottó, Imre
            AddMother(People.First(person => person.ID == 21), People.First(person => person.ID == 23));
            AddMother(People.First(person => person.ID == 22), People.First(person => person.ID == 23));
            // Piast - Géza, László, Lampert, Zsófia, Eufémia, Ilona
            AddMother(People.First(person => person.ID == 25), People.First(person => person.ID == 24));
            AddMother(People.First(person => person.ID == 26), People.First(person => person.ID == 24));
            AddMother(People.First(person => person.ID == 27), People.First(person => person.ID == 24));
            AddMother(People.First(person => person.ID == 28), People.First(person => person.ID == 24));
            AddMother(People.First(person => person.ID == 29), People.First(person => person.ID == 24));
            AddMother(People.First(person => person.ID == 30), People.First(person => person.ID == 24));
            // párok
            // Taksony - ??
            AddPair(People.First(person => person.ID == 1), People.First(person => person.ID == 2));
            // Mihály - ??
            AddPair(People.First(person => person.ID == 3), People.First(person => person.ID == 7));
            // Géza - Sarolt
            AddPair(People.First(person => person.ID == 4), People.First(person => person.ID == 12));
            // Szár László - Premiszláva
            AddPair(People.First(person => person.ID == 5), People.First(person => person.ID == 13));
            // Vazul - Katun
            AddPair(People.First(person => person.ID == 6), People.First(person => person.ID == 15));
            // Boleszláv - Judit
            AddPair(People.First(person => person.ID == 19), People.First(person => person.ID == 9));
            // Géza - Gizella
            AddPair(People.First(person => person.ID == 25), People.First(person => person.ID == 23));
            // Béla - Piast
            AddPair(People.First(person => person.ID == 24), People.First(person => person.ID == 17));
        }

        public override async Task<Business.Person> CreateAsync(Business.Person content)
        {
            return await Task.Run(() =>
            {
                content.ID = id;
                id++;
                People.Add(content);
                return content;
            });
        }

        public void AddMother(Business.Person child, Business.Person mother)
        {
            child.Mother = mother;
            mother.Children.Add(child);
        }

        public void AddFather(Business.Person child, Business.Person father)
        {
            child.Father = father;
            father.Children.Add(child);
        }

        public void AddPair(Business.Person one, Business.Person two)
        {
            one.Partner = two;
            two.Partner = one;
        }

        public override async Task DeleteAsync(int id)
        {
            await Task.Run(() =>
            {
                foreach (var person in People)
                {
                    if (person.ID == id)
                    {
                        People.Remove(person);
                        break;
                    }
                }
            });
        }

        public override async Task<IEnumerable<Business.Person>> GetAllAsync()
        {
            var result = await Task.Run<IEnumerable<Business.Person>>(() => People);

            return result;
        }

        public override async Task<Business.Person> GetAsync(int id)
        {
            var result = await Task.Run(() => People.ElementAt(id));

            return result;
        }

        public override async Task ModifyAsync(Business.Person content)
        {
            await Task.Run(() =>
            {
                var original = People.First(person => person.ID.Equals(content.ID));
                original.FirstName = content.FirstName;
                original.LastName = content.LastName;
                original.DateOfBirth = content.DateOfBirth;
                original.DateOfDeath = content.DateOfDeath;
                original.Gender = content.Gender;
            });
        }
    }
}
