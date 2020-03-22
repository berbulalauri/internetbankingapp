using BBS.ConsumerServicesAPI.ApiModels;
using BBS.ConsumerServicesAPI.Repositories.Abstractions;
using BBS.ConsumerServicesAPI.Repositories.BaseModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.ConsumerServicesAPI.Services.concrete
{
    public class BusService : IBusService
    {
        private readonly IBusRepository _context;
        public BusService(IBusRepository context)
        {
            _context = context;
        }

        public async Task<Bus> GetBusById(int? id)
        {
            return await _context.GetByIdAsync(id);
        }

        public async Task<List<Bus>> GetBuses()
        {
            return await _context.GetAllAsync();
        }
        public async Task Add(Bus bus)
        {
            await _context.InsertAsync(bus);
            await _context.SaveAsync();
        }
        public async Task Update(Bus bus)
        {
            await _context.UpdateAsync(bus);
            await _context.SaveAsync();
        }
        public async Task<bool> Delete(int id)
        {
            var isBusExists = await _context.GetByIdAsync(id);
            if (isBusExists != null)
            {
                await _context.DeleteAsync(id);
                await _context.SaveAsync();
                return true;
            }
            return false;
        }

    }
}
