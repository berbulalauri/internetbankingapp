using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BBS.Models.Models;

namespace BBS.DAL.Repositories
{
    public interface IBaseRepository<T> where T : class , ISoftDeletable
    {
        Task<List<T>>GetAllAsync();
        Task<List<T>> FindAllAsync(Expression<Func<T,bool>> predicate);
        Task<T> GetByIdAsync(object id);
        Task InsertAsync(T obj);
        Task UpdateAsync(T obj);
        Task DeleteAsync(object id);
        Task SaveAsync();

    }
}
