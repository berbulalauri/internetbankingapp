using BBS.ConsumerServicesAPI.Repositories.Abstractions;
using BBS.ConsumerServicesAPI.Repositories.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.ConsumerServicesAPI.Services.concrete
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<IEnumerable<Ticket>> GetTickets()
        {
            return await _ticketRepository.GetAllAsync();
        }

        public async Task Create(Ticket ticket)
        {
            await _ticketRepository.InsertAsync(ticket);
        }

        public async Task SaveAsync()
        {
            await _ticketRepository.SaveAsync();
        }
    }
}
