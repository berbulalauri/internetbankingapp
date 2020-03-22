using BBS.DAL.Clients.Base;
using BBS.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.DAL.Clients.Abstract
{
    public interface ITicketClient : IBaseWebClient<Ticket>
    {
    }
}
