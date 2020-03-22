using BBS.DAL.Database;
using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.DAL.Repositories
{
    public class EmploymentRepository : BaseRepository<Employment>, IEmploymentRepository
    {
        public EmploymentRepository(BankDbContext ctx) : base(ctx)
        {
        }
    }
}

