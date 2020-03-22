using BBS.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.DAL.Clients.Base
{
    public interface IBaseWebClient<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetById(int id);
        Task Create(T obj);
    }
}
