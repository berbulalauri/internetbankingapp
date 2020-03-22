using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.DAL.Repositories
{
    public interface ICurrencyRepository: IBaseRepository<Currency>
    {
        Currency GetDefaultCurrency();
    }
}
