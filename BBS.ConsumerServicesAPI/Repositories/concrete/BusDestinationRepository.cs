using BBS.ConsumerServicesAPI.DAL;
using BBS.ConsumerServicesAPI.Repositories.BaseModels;
using BBS.ConsumerServicesAPI.Repositories.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BBS.ConsumerServicesAPI.Repositories.Concrete
{
    public class BusDestinationRepository : BaseRepository<Destination>, IBusDestinationRepository
    {
        public BusDestinationRepository(ConsumerServiceDbContext ctx) : base(ctx)
        {
        }

        public async Task<Destination> DeleteBusDestination(int id)
        {
            var busDestination = await _context.Destinations.FindAsync(id);
            if (busDestination != null) 
            {
                busDestination.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
            
            return busDestination;
        }

        public async Task<List<Destination>> GetAllBusDestinations()
        {
            return await _context.Destinations.Select(x=> x).ToListAsync();
        }

        public async Task<Destination> UpdateBusDestination(Destination destination)
        {
            _context.Destinations.Update(destination);
            _context.Entry(destination).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                _context.SaveChanges();
            }

            return destination;
        }
        public async Task<Destination> AddBusDestinationRepo(Destination destination)
        {
            _context.Destinations.Add(destination);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                _context.SaveChanges();
            }

            return destination;
        }
    }
}
