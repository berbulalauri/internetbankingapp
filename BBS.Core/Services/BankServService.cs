using BBS.DAL.Repositories;
using BBS.Interfaces.Services;
using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Core.Services
{
	public class BankServService : IBankServService
	{
		private IServiceRepository serviceRepo;

		public BankServService(IServiceRepository service)
		{
			serviceRepo = service;
		}

		public async Task<Service> GetServiceByNameAsync(string name)
		{
			return (await serviceRepo.GetAllAsync()).FirstOrDefault(x=>x.Name==name);
		}
	}
}
