using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManagementAPI.Models.Requests
{
    public class UserRequest
    {
        public string UserId { get; set; }
        public bool RemoveAssignment { get; set; } = false;
    }
}
