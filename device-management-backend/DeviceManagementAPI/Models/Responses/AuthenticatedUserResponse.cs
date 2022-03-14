using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManagementAPI.Models.Responses
{
    public class AuthenticatedUserResponse
    {
        public bool Success { get; set; }
        public string AccessToken { get; set; }
    }
}
