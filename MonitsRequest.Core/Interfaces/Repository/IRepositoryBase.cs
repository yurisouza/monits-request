using System;
using System.Collections.Generic;
using System.Text;

namespace MonitsRequest.Core.Interfaces.Repository
{
    public interface IRepositoryBase<T>
    {
        T Get(Guid key);
        T Insert(T entity);
        T Update(T entity);
        bool Delete(Guid key);
    }
}
