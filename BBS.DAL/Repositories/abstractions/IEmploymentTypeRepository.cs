using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.DAL.Repositories
{
    public interface IEmploymentTypeRepository : IBaseRepository<EmploymentType>
    {
        public IEnumerable<EmploymentType> GetAll();
    }
}

