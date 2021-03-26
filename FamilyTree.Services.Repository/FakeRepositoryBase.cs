using FamilyTree.Business;
using FamilyTree.Services.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Services.Repository
{
    /// <summary>
    /// Ez lesz használatban akkor, amikor adatbázis nélkül használjuk a rendszert.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FakeRepositoryBase<T> : IAsyncRepository<T> where T : BusinessBase
    {
        public ObservableCollection<T> Collection { get; set; }

        public Token Token { get; set; }

        public string Uri { get; set; }

        public FakeRepositoryBase()
        {
            Collection = new ObservableCollection<T>();
        }

        public FakeRepositoryBase(string uri, Token token)
        {
            _ = uri;
            _ = token;

            Collection = new ObservableCollection<T>();
        }

        public virtual async Task<T> CreateAsync(T content)
        {
            return await Task.Run(() =>
            {
                Collection.Add(content);
                return content;
            });
        }

        public virtual async Task DeleteAsync(string id)
        {
            await Task.Run(() =>
            {
                foreach (var element in Collection)
                {
                    if (element.ID == id)
                    {
                        Collection.Remove(element);
                        break;
                    }
                }
            });
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                return Collection;
            });
        }

        public virtual async Task<T> GetAsync(string id)
        {
            return await Task.Run(() =>
            {
                foreach (var element in Collection)
                {
                    if (element.ID == id)
                        return element;
                }
                return null;
            });
        }

        public virtual async Task ModifyAsync(T content)
        {
            await Task.Run(() =>
            {
                var old = content;

                foreach (var element in Collection)
                {
                    if(element.ID == content.ID)
                    {
                        old = element;
                        break;
                    }
                }

                if (old == content) return;

                Collection.Remove(old);
                Collection.Add(content);
            });
        }
    }
}
