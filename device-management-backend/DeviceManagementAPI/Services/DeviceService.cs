using DeviceManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using DeviceManagementAPI.Interfaces;

namespace DeviceManagementAPI.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _devicesRepo;

        public DeviceService(IDeviceRepository devicesRepository)
        {
            _devicesRepo = devicesRepository;
        }

        public async Task Create(Device device)
        {
            await _devicesRepo.CreateAsync(device);
        }

        public async Task<List<Device>> Get()
        {
            var devices = await _devicesRepo.GetAllAsync();
            return devices;
        }

        public async Task<Device> Get(string id)
        {
            var device = await _devicesRepo.GetByIdAsync(id);
            return device;
        }

        public async Task Delete(string id)
        {
            await _devicesRepo.DeleteAsync(id);
        }

        public async Task Update(string id, Device device)
        {
            await _devicesRepo.UpdateAsync(id, device);
        }
    }
}
