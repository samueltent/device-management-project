using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManagementAPI.Models.Requests
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Location { get; set; }
    }
}
