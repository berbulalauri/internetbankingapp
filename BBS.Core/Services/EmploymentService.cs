using BBS.DAL.Repositories;
using BBS.Interfaces.Services;
using BBS.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BBS.Core.Services
{
    public class EmploymentService : IEmploymentService
    {
        private readonly IEmploymentRepository _employmentRepository;

        public EmploymentService(IEmploymentRepository employmentRepository)
        {
            _employmentRepository = employmentRepository;
        }
        public async Task CreateEmployment(Employment employment)
        {
            await _employmentRepository.InsertAsync(employment);
            await _employmentRepository.SaveAsync();
        }

        public async Task<IEnumerable<Employment>> GetEmployments()
        {
            return await _employmentRepository.GetAllAsync();
        }
    }
}
