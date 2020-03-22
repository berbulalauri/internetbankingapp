using BBS.ConsumerServicesAPI.DAL;
using BBS.ConsumerServicesAPI.Repositories.BaseModels;
using BBS.ConsumerServicesAPI.Repositories.Abstractions;

namespace BBS.ConsumerServicesAPI.Repositories.Concrete
{
    public class BusRepository : BaseRepository<Bus>, IBusRepository
    {
        public BusRepository(ConsumerServiceDbContext ctx) : base(ctx)
        {
        }
    }
}
