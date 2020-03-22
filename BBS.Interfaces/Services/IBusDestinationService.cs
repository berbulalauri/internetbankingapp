using BBS.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Interfaces.Services
{
    public interface IBusDestinationService
    {
        Task<IEnumerable<Destination>> GetDestinationsAsync();
    }
}
