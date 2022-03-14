using DeviceManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManagementAPI.Services
{
    public interface IDeviceService
    {
        Task<List<Device>> Get();
        Task<Device> Get(string id);
        Task Create(Device device);
        Task Update(string id, Device device);
        Task Delete(string id);
    }
}
