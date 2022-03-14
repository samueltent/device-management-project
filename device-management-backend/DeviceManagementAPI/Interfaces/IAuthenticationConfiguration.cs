﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManagementAPI.Interfaces
{
    public interface IAuthenticationConfiguration
    {
        string AccessTokenSecret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
