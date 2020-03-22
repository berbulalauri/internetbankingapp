using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.DAL.Repositories.abstractions
{
    public interface ICardRequestRepository : IBaseRepository<CardRequest>
    {
        public Task<List<CardRequest>> GetAllIncludesAsync();
        public Task<CardRequest> GetByIdIncludesAsync(int id);
        Task<CardRequest> GetByUserIdAsync(int? id);
    }
}
