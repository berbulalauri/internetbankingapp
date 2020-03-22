using BBS.ConsumerServicesAPI.ApiModels;
using BBS.ConsumerServicesAPI.Repositories.BaseModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.ConsumerServicesAPI.Services
{
    public interface IBusService
    {
        public  Task<List<Bus>> GetBuses();
        public Task<Bus> GetBusById(int? id);
        public Task Add(Bus bus);
        public Task Update(Bus bus);
        public Task<bool> Delete(int id);
    }
}
