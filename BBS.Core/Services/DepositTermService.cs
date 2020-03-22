using BBS.DAL.Repositories.abstractions;
using BBS.Interfaces;
using BBS.Interfaces.Services;
using BBS.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BBS.Core.Services
{
    public class DepositTermService : IDepositTermService
    {
        private readonly IDepositTermRepository _repo;
        private ILogger _logger;
        public DepositTermService(IDepositTermRepository repository, ILogger logger)
        {
            _repo = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<DepositTerm>> GetAllWithTerm()
        {
            return await _repo.GetAllAsync();
        }
    }
}
