using BBS.ConsumerServicesAPI.DAL;
using BBS.ConsumerServicesAPI.Repositories.BaseModels;
using BBS.ConsumerServicesAPI.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Db = BBS.ConsumerServicesAPI.Repositories.BaseModels;
using Api = BBS.ConsumerServicesAPI.ApiModels;
using BBS.ConsumerServicesAPI.Helpers;

namespace BBS.ConsumerServicesAPI.Repositories.concrete
{
    public class TripRepository : BaseRepository<Db.Trip>, ITripRepository
    {
        public TripRepository(ConsumerServiceDbContext ctx) : base(ctx)
        {
        }

        public async Task<Db.Trip> Create(Api.Trip trip)
        {
            var tripToAdd = ModelTransformHelper.ApiToTripDb(trip);
            try
            {
                _context.Add(tripToAdd);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                _context.SaveChanges();
            }

            return tripToAdd;
        }

        public async Task<List<Db.Trip>> GetAllIncludes()
        {
            return await _context.Trips.Include(t => t.DestinationFrom).Include(t => t.DestinationTo).Include(t => t.Bus).ToListAsync();
        }

        public async Task<Db.Trip> GetOneIncludes(int id)
        {
            return await _context.Trips.Where(t=>t.Id==id).Include(t => t.DestinationFrom).Include(t => t.DestinationTo).Include(t=>t.Bus).FirstOrDefaultAsync();
        }
    }
}
