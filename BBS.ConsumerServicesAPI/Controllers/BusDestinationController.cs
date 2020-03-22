using System.Collections.Generic;
using System.Threading.Tasks;
using BBS.ConsumerServicesAPI.Repositories.BaseModels;
using BBS.ConsumerServicesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BBS.ConsumerServicesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusDestinationController : ControllerBase
    {
        private readonly IBusDestinationService _busDestinationService;

        public BusDestinationController(IBusDestinationService busDestinationService)
        {
            _busDestinationService = busDestinationService;
        }

        [HttpGet]
        public async Task<IEnumerable<Destination>> Get()
        {
            return await _busDestinationService.GetAllBusDestinations();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Destination>> DeleteBusDestination(int id)
        {
            var busDestinationToDelete = await _busDestinationService.GetBusDestination(id);

            if (busDestinationToDelete != null)
            {
                await _busDestinationService.DeleteBusDestination(id);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Destination>> AddBusDestination(Destination destination)
        {
            var result = await _busDestinationService.AddBusDestination(destination);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Destination>> UpdateBusDestination(Destination destination)
        {
            var result = await _busDestinationService.UpdateBusDestination(destination);

            if (result == null) 
            {
                return BadRequest();
            }

            return Ok(result);
        }

    }
}