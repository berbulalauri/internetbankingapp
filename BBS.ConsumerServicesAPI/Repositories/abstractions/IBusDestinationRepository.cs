using BBS.ConsumerServicesAPI.DAL.Repositories;
using BBS.ConsumerServicesAPI.Repositories.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.ConsumerServicesAPI.Repositories.Abstractions
{
	public interface IBusDestinationRepository : IBaseRepository<Destination>
	{
		public Task<List<Destination>> GetAllBusDestinations();
		public Task<Destination> UpdateBusDestination(Destination destination);
		public Task<Destination> AddBusDestinationRepo(Destination destination);
		public Task<Destination> DeleteBusDestination(int id);
	}
}
