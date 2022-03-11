using DeviceManagementAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManagementAPI.Models
{
    public class DbSettings : IDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string DevicesCollectionName { get; set; }
        public string UsersCollectionName { get; set; }
    }
}
