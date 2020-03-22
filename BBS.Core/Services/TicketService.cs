using BBS.DAL.Clients.Abstract;
using BBS.Interfaces.Services;
using BBS.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Core.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketClient _ticketClient;

        public TicketService(ITicketClient ticketClient)
        {
            _ticketClient = ticketClient;
        }

        public async Task Create(Ticket ticket)
        {
            await _ticketClient.Create(ticket);
        }
    }
}
