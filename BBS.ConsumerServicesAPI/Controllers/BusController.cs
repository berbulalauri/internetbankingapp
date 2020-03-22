using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBS.ConsumerServicesAPI.ApiModels;
using BBS.ConsumerServicesAPI.Repositories.BaseModels;
using BBS.ConsumerServicesAPI.Services;
using BBS.ConsumerServicesAPI.Services.concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BBS.Models.Constants;
namespace BBS.ConsumerServicesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusController : ControllerBase
    {
        private readonly IBusService _busService;

        public BusController(IBusService busService)
        {
            _busService = busService;
        }

        [HttpGet]
        public async Task<IEnumerable<Bus>> Get()
        {
            return await _busService.GetBuses();
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<Bus> GetById(int id)
        {
            return await _busService.GetBusById(id);
        }
        [HttpPost]
        public async Task<IActionResult> Post(BusResourceModel bus)
        {
            if (ModelState.IsValid && bus.CountOfSeats > 0)
            {
                Bus newBus = new Bus
                {
                    Model = bus.Model,
                    SeatsCount = bus.CountOfSeats,
                };
                await _busService.Add(newBus);
                return Ok();
            }
            return BadRequest(ErrorMessages.InvalidApiModel);
        }
        [HttpPut]
        public async Task<IActionResult> Put(Bus bus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorMessages.InvalidApiModel);
            }

            var existingBus = await _busService.GetBusById(bus.Id);

            if (existingBus != null)
            {
                existingBus.SeatsCount = bus.SeatsCount;
                existingBus.Model = bus.Model;

                await _busService.Update(existingBus);
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bus = await _busService.GetBusById(id);

            if (bus != null)
            {
                await _busService.Delete(id);
                return Ok();
            }

            return BadRequest(ErrorMessages.InvalidBusId);

        }

    }
}