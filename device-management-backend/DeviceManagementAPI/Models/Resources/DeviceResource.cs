using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManagementAPI.Models.Resources
{
    public class DeviceResource
    {
        public bool Success { get; set; } = true;
        public List<Device> Devices {get; set;}
    }
}
