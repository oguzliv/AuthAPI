using Auth.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.DataAcces.Repository
{
    public interface IBaseRepository<T> where T : User
    {
        Task<T> Create(T entity);
        Task<T> GetById(Guid Id);
        Task<T> Get();
        Task<T> Update(T entity);
        Task<T> Delete(T entity);

    }
}
