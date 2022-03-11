using DeviceManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManagementAPI.Interfaces
{
    public interface IDeviceRepository
    {
        Task<List<Device>> GetAllAsync();
        Task<Device> GetByIdAsync(string id);
        Task CreateAsync(Device device);
        Task UpdateAsync(string id, Device device);
        Task DeleteAsync(string id);
    }
}
