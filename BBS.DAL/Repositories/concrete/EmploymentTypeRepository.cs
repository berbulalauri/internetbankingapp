using BBS.DAL.Database;
using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBS.DAL.Repositories
{
    public class EmploymentTypeRepository : BaseRepository<EmploymentType>, IEmploymentTypeRepository
    {
        public EmploymentTypeRepository(BankDbContext ctx) : base(ctx)
        {
        }

        public IEnumerable<EmploymentType> GetAll()
        {
            var employmentList = _context.EmploymentTypes.ToList();

            return employmentList;
        }
    }
}

