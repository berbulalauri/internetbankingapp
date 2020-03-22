using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BBS.Interfaces.Services
{
	public interface IDepositService : IDisposable
	{
		public Task<IEnumerable<DepositType>> GetAllTypes();
		public Task<DepositType> GetType(int Id);
		Task CreateDepositType(DepositType depositType);
		Task Add(Deposit deposit);
		public Task Delete(int id);
		public Task<IEnumerable<DepositType>> GetAllWithTerm();
		public Task<List<DepositType>> GetWithFilter(decimal amountString, int currencyString, int termString);
		public Task<DepositType> GetDepositTypeService(int id);
		public Task UpdateDepositTypeService(DepositType depositType);
		public Task AddDepositPercents();
		public Task<bool> OpenDepositAsync(Deposit deposit, int id, Account account);
        public Task<List<DepositType>> Filter(string sortOrder, string columnName, string searchString);
    }
   
}
