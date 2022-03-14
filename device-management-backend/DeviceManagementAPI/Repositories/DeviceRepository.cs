using DeviceManagementAPI.Interfaces;
using DeviceManagementAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManagementAPI.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly IMongoCollection<Device> _devices;

        public DeviceRepository(IDbSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _devices = database.GetCollection<Device>(settings.DevicesCollectionName);
        }

        public async Task<List<Device>> GetAllAsync()
        {
            return await _devices.Find(device => true).ToListAsync<Device>();
        }
        public async Task<Device> GetByIdAsync(string id)
        {
            return await _devices.Find(device => device.Id == id).FirstOrDefaultAsync<Device>();
        }

        public async Task CreateAsync(Device device)
        {
            await _devices.InsertOneAsync(device);
        }

        public async Task UpdateAsync(string id, Device device)
        {
            await _devices.ReplaceOneAsync(device => device.Id == id, device);
        }

        public async Task DeleteAsync(string id)
        {
            await _devices.DeleteOneAsync(device => device.Id == id);
        }
    }
}
