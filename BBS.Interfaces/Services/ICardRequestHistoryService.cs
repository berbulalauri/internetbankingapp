
using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Interfaces.Services
{
    public interface ICardRequestHistoryService
    {
        public Task<List<CardRequestHistory>> GetAll();
        public Task<List<CardRequestHistory>> GetAllIncludes();
        public Task<List<CardRequestHistory>> GetAllByRequestIdIncludes(int id);
        public Task<CardRequestHistory> GetById(int? id);
        public Task Add(CardRequestHistory cardRequestHistory);
        public Task Update(CardRequestHistory cardRequestHistory);
        public Task Delete(int id);
        public Task MakeHistory(int userId);
    }
}