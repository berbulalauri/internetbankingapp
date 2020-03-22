using BBS.ConsumerServicesAPI.DAL.Repositories;
using BBS.ConsumerServicesAPI.Repositories.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Db = BBS.ConsumerServicesAPI.Repositories.BaseModels;
using Api = BBS.ConsumerServicesAPI.ApiModels;

namespace BBS.ConsumerServicesAPI.Repositories.Abstractions
{
	public interface ITripRepository : IBaseRepository<Trip>
	{
		public Task<List<Trip>> GetAllIncludes();
		public Task<Trip> GetOneIncludes(int id);
		public Task<Db.Trip> Create(Api.Trip trip);
	}
}
