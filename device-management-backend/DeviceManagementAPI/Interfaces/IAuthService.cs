using DeviceManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManagementAPI.Interfaces
{
    public interface IAuthService
    {
        string GetHashedPassword(string password);
        bool VerifyPassword(string password, string hash);
        string GenerateToken(User user);
    }
}
