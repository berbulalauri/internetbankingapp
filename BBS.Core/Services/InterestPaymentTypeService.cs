using BBS.DAL.Repositories;
using BBS.Interfaces.Services;
using BBS.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BBS.Core.Services
{
    public class InterestPaymentTypeService : IInterestPaymentTypeService
    {
        private readonly IInterestPaymentTypeRepository _repo;

        public InterestPaymentTypeService(IInterestPaymentTypeRepository repository)
        {
            _repo = repository;
        }

        public async Task<IEnumerable<InterestPaymentType>> GetAll()
        {
            return await _repo.GetAllAsync();
        }
    }
}
