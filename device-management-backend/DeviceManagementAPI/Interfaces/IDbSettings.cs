using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManagementAPI.Interfaces
{
    public interface IDbSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string DevicesCollectionName { get; set; }
        string UsersCollectionName { get; set; }

    }
}
