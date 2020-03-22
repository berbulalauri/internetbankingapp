using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBS.ConsumerServicesAPI.Repositories.BaseModels;
using BBS.ConsumerServicesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BBS.ConsumerServicesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task<IEnumerable<Ticket>> GetTickets()
        {
            return await _ticketService.GetTickets();
        }

        [HttpPost]
        public async Task PostTicket(Ticket ticket)
        {
            await _ticketService.Create(ticket);
            await _ticketService.SaveAsync();
        }
    }
}