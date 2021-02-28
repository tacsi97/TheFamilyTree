﻿using FamilyTree.Business;
using FamilyTree.Services.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.Services.Repository
{
    /// <summary>
    /// Ez lesz használatban akkor, amikor a helyi gépen futó adatbázishoz szeretnénk hozzáférni.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class LocalRepositoryBase<T> : IAsyncRepository<T>
    {
        #region Properties

        public Token Token { get; set; }
        public string Uri { get; set; }

        #endregion

        #region Constructor

        public LocalRepositoryBase(string uri, Token token)
        {
            Uri = uri;
            Token = token;
        }

        public abstract Task<IEnumerable<T>> GetAllAsync();

        public abstract Task<T> GetAsync(int id);

        public abstract Task CreateAsync(T content);

        public abstract Task ModifyAsync(T content);

        public abstract Task DeleteAsync(int id);

        #endregion
    }
}