using MonitsRequest.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MonitsRequest.Core.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly IDbConnection _connection;

        public RepositoryBase(IDbConnection connection)
        {
            this._connection = connection;
        }

        public abstract T Get(Guid key);
        public abstract T Insert(T entity);
        public abstract T Update(T entity);
        public abstract bool Delete(Guid key);
    }
}
