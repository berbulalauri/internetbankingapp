using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.DAL.Repositories.abstractions
{
    public interface ICardRequestHistoryRepository : IBaseRepository<CardRequestHistory>
    {
        public Task<List<CardRequestHistory>> GetAllByRequestIdIncludesAsync(int id);
    }
}