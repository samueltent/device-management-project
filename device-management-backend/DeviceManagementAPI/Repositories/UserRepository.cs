using DeviceManagementAPI.Interfaces;
using DeviceManagementAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManagementAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;
        public UserRepository(IDbSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.UsersCollectionName);
        }
        public async Task CreateAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task DeleteAsync(string id)
        {
            await _users.DeleteOneAsync(user => user.Id == id);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _users.Find(user => true).ToListAsync<User>();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _users.Find(user => user.Id == id).FirstOrDefaultAsync<User>();
        }

        public async Task UpdateAsync(string id, User user)
        {
            await _users.ReplaceOneAsync(user => user.Id == id, user);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _users.Find(user => user.Email == email).FirstOrDefaultAsync<User>();
        }
    }
}
