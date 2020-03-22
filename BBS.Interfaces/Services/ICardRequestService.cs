using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Interfaces.Services
{
    public interface ICardRequestService
    {
        public Task<List<CardRequest>> GetAll();
        public Task<List<CardRequest>> GetAllIncludes();

        public Task<CardRequest> GetById(int? id);
        public Task<CardRequest> GetByUserId(int? id);

        public Task<CardRequest> GetByIdIncludes(int id);
        public Task Add(CardRequest cardRequest);
        public Task Update(CardRequest cardRequest);
        public Task Delete(int id);
    }
}
