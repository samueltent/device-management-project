using DeviceManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManagementAPI.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> Get();
        Task<User> Get(string id);
        Task Create(User user);
        Task Update(string id, User user);
        Task Delete(string id);
    }
}
