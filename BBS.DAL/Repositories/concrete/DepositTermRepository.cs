using BBS.DAL.Database;
using BBS.DAL.Repositories.abstractions;
using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.DAL.Repositories.concrete
{
    public class DepositTermRepository : BaseRepository<DepositTerm>, IDepositTermRepository
    {
        public DepositTermRepository(BankDbContext ctx) : base(ctx)
        {
        }
    }
}
