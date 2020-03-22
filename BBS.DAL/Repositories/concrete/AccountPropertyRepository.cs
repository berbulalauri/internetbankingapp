using BBS.DAL.Database;
using BBS.DAL.Repositories.abstractions;
using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS.DAL.Repositories.concrete
{
    public class AccountPropertyRepository : BaseRepository<AccountProperty>, IAccountPropertyRepository
    {
        public AccountPropertyRepository(BankDbContext ctx) : base(ctx)
        {
        }

        public AccountProperty GetPropertyModel()
        {
            return _context.AccountProperties.FirstOrDefault(a => a.IsDeleted == false);
        }
    }
}
