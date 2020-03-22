using BBS.Models.Models;

namespace BBS.DAL.Repositories
{
    public interface IServiceRepository : IBaseRepository<Service>
    {
        int? GetReceiverAccountIdRepo(int serviceId);
        string GetReceiverNameRepo(int serviceId);
        Service GetServiceByName(string name);
    }
}
