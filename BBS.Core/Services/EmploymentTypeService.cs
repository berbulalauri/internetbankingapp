using BBS.DAL.Repositories;
using BBS.Interfaces.Services;
using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Core.Services
{
    public class EmploymentTypeService : IEmploymentTypeService
	{
		private readonly IEmploymentTypeRepository _EmploymentTypeRepository;
		public EmploymentTypeService(IEmploymentTypeRepository employmentTypeRepository)
		{
			_EmploymentTypeRepository = employmentTypeRepository;
		}

		public IEnumerable<EmploymentType> GetAllTypes()
		{
			return _EmploymentTypeRepository.GetAll();
		}
	}
}
