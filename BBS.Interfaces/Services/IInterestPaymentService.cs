using BBS.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BBS.Interfaces.Services
{
    public interface IInterestPaymentTypeService
    {
        Task<IEnumerable<InterestPaymentType>> GetAll();
    }
}
