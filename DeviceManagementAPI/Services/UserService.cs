using DeviceManagementAPI.Interfaces;
using DeviceManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManagementAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _usersRepo;

        public UserService(IUserRepository usersRepository)
        {
            _usersRepo = usersRepository;
        }
        public async Task Create(User user)
        {
            await _usersRepo.CreateAsync(user);
        }

        public async Task Delete(string id)
        {
            await _usersRepo.DeleteAsync(id);
        }

        public async Task<List<User>> Get()
        {
            return await _usersRepo.GetAllAsync();
        }

        public async Task<User> Get(string id)
        {
            return await _usersRepo.GetByIdAsync(id);
        }

        public async Task Update(string id, User user)
        {
            await _usersRepo.UpdateAsync(id, user);
        }
    }
}
