using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManagementAPI.Models.Resources
{
    public class UserResource
    {
        public bool Success { get; set; } = true;
        public List<User> Users { get; set; }
    }
}
