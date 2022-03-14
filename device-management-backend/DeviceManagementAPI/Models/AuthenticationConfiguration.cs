using DeviceManagementAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManagementAPI.Models
{
    public class AuthenticationConfiguration : IAuthenticationConfiguration
    {
        public string AccessTokenSecret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
