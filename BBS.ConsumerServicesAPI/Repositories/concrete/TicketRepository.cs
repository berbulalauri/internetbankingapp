using BBS.ConsumerServicesAPI.DAL;
using BBS.ConsumerServicesAPI.Repositories.BaseModels;
using BBS.ConsumerServicesAPI.Repositories.Abstractions;
using System.Threading.Tasks;

namespace BBS.ConsumerServicesAPI.Repositories.concrete
{
    public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(ConsumerServiceDbContext ctx) : base(ctx)
        {
        }
    }
}
