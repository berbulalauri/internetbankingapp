using BBS.DAL.Repositories;
using BBS.Interfaces.Services;
using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Core.Services
{
    public class ServiceService : IServiceService
    {
		IServiceRepository _serviceRepository;

		public ServiceService(IServiceRepository serviceRepository)
		{
			_serviceRepository = serviceRepository;
		}

		public async Task<Service> GetServiceAsync(int id)
		{
			return await _serviceRepository.GetByIdAsync(id);
		}
	}
}
