using Api = BBS.ConsumerServicesAPI.ApiModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Db = BBS.ConsumerServicesAPI.Repositories.BaseModels;

namespace BBS.ConsumerServicesAPI.Services
{
	public interface ITripService
	{
		Task<List<Api.Trip>> GetAllFiltered(string from, string to, DateTime? departure); 
		Task<Api.Trip> GetTripWithSeats(int id);
		Task<ActionResult<Db.Trip>> Create(Api.Trip trip);
	}
}
