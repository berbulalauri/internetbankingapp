using BBS.DAL.Database;
using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.DAL.Repositories
{
    public class ServiceCategoryRepository : BaseRepository<ServiceCategory>, IServiceCategoryRepository
    {
        public ServiceCategoryRepository(BankDbContext ctx) : base(ctx)
        {
        }
    }
}
