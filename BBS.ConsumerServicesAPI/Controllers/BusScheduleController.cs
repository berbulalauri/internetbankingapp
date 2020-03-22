using Api = BBS.ConsumerServicesAPI.ApiModels;
using BBS.ConsumerServicesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Db = BBS.ConsumerServicesAPI.Repositories.BaseModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BBS.ConsumerServicesAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BusScheduleController : ControllerBase
	{
		private readonly ITripService _tripService;

		public BusScheduleController(ITripService trip)
		{
			_tripService = trip;
		}

		[HttpGet]
		public async Task<IEnumerable<Api.Trip>> Get(string from, string to, DateTime? departure)
		{
			return await _tripService.GetAllFiltered(from, to, departure);
		}

		[HttpPost]
		public async Task<ActionResult<Db.Trip>> Create([FromBody] Api.Trip trip)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var tripAdded = await _tripService.Create(trip);
					return Ok(tripAdded);
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
			else 
			{
				return BadRequest(ModelState.Values);
			}
		}

		[HttpGet("{id}")]
		public async Task<Api.Trip> GetById(int id)
		{
			return await _tripService.GetTripWithSeats(id);
		}
	}
}