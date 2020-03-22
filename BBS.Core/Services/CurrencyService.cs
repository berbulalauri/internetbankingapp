using BBS.DAL.Repositories;
using BBS.Interfaces.Services;
using BBS.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BBS.Core.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;
        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public Currency GetDefaultCurrency()
        {
            return _currencyRepository.GetDefaultCurrency();
        }

        public async Task<IEnumerable<Currency>> GetAll()
        {
            return await _currencyRepository.GetAllAsync();
        }
    }
}
