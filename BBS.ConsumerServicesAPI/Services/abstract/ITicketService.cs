using BBS.ConsumerServicesAPI.Repositories.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.ConsumerServicesAPI.Services
{
    public interface ITicketService
    {
        public Task<IEnumerable<Ticket>> GetTickets();
        Task Create(Ticket ticket);
        Task SaveAsync();
    }
}
