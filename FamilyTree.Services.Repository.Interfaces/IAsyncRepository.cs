using FamilyTree.Business;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Services.Repository.Interfaces
{
    /// <summary>
    /// This is an interface for the main CRUD operations.
    /// </summary>
    /// <typeparam name="T">The generic type</typeparam>
    public interface IAsyncRepository<T>
    {
        /// <summary>
        /// A token for accessing the database.
        /// It contains a username and a password
        /// </summary>
        public Token Token { get; set; }

        /// <summary>
        /// An uri that specifies the the database.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// This function returns all of the <typeparamref name="T"/> objects.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// This function returns a <typeparamref name="T"/> object, which is a <typeparamref name="BusinessBase"/>,
        /// where the specified <paramref name="id"/> is equals with the <typeparamref name="BusinessBase"/> objects <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The ID of the <typeparamref name="BusinessBase"/> object.</param>
        /// <returns>The object with the corresponding ID or null if there isn't any object</returns>
        Task<T> GetAsync(string id);

        /// <summary>
        /// This function inserts a <typeparamref name="T"/> object to the dataset, 
        /// and it sends the <typeparamref name="T"/> object back.
        /// </summary>
        /// <param name="content">The <typeparamref name="T"/> object</param>
        /// <returns>The <typeparamref name="T"/> object that is created.</returns>
        Task<T> CreateAsync(T content);

        /// <summary>
        /// This function finds the <typeparamref name="T"/> object
        /// which has the same id as the <typeparamref name="T"/> object given in the parameter.
        /// </summary>
        /// <param name="content">The <typeparamref name="T"/> object with the new values</param>
        /// <returns></returns>
        Task ModifyAsync(T content);

        /// <summary>
        /// This function removes the <typeparamref name="T"/> object with the given <paramref name="id"/>
        /// </summary>
        /// <param name="id">The ID of the <typeparamref name="T"/> object, you want to remove</param>
        /// <returns></returns>
        Task DeleteAsync(string id);
    }
}
