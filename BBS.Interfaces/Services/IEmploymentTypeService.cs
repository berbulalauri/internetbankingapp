using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Interfaces.Services
{
    public interface IEmploymentTypeService
    {
        public IEnumerable<EmploymentType> GetAllTypes();
    }
}
