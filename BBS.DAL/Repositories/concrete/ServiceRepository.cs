using BBS.DAL.Database;
using BBS.Models.Models;
using System.Linq;

namespace BBS.DAL.Repositories
{
    public class ServiceRepository : BaseRepository<Service>, IServiceRepository
    {
        public ServiceRepository(BankDbContext ctx) : base(ctx)
        {
        }
        public int? GetReceiverAccountIdRepo(int serviceId)
        {
            return _context.Services.Where(x => x.Id == serviceId).Select(s => s.AccountId).FirstOrDefault();
        }
        public string GetReceiverNameRepo(int serviceId)
        {
            return _context.Services.Where(x => x.Id == serviceId).Select(s => s.Name).FirstOrDefault();
        }

        public Service GetServiceByName(string name)
        {
            return _context.Services.Where(x => !x.IsDeleted && x.Name == name).FirstOrDefault();
        }
    }
}

