using BBS.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BBS.Interfaces.Services
{
    public interface IEmploymentService
    {
        Task CreateEmployment(Employment employment);
        Task<IEnumerable<Employment>> GetEmployments();
    }
}
