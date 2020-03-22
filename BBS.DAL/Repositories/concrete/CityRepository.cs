using BBS.DAL.Database;
using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.DAL.Repositories
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(BankDbContext ctx) : base(ctx)
        {
        }

        public Task<List<City>> GetCitiesWithTrackingAsync()
        {
            return _context.Cities.Where(x => !x.IsDeleted).ToListAsync();
        }
    }
}


