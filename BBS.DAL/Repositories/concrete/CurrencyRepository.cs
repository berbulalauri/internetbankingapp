using BBS.DAL.Constants;
using BBS.DAL.Database;
using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBS.DAL.Repositories
{
    public class CurrencyRepository : BaseRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(BankDbContext ctx) : base(ctx)
        {
        }

        public Currency GetDefaultCurrency()
        {
            return _context.Currencies.FirstOrDefault(c => c.Name == CurrencyConstants.DefaultCurrency);
        }
    }
}

