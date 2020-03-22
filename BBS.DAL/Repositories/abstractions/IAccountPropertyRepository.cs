using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.DAL.Repositories
{
    public interface IAccountPropertyRepository : IBaseRepository<AccountProperty>
    {
        AccountProperty GetPropertyModel();
    }
}
