using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeviceManagementAPI.Models
{
    public class Device
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("manufacturer")]
        public string Manufacturer { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("os")]
        public string Os { get; set; }

        [BsonElement("osVersion")]
        public int OsVersion { get; set; }

        [BsonElement("processor")]
        public string Processor { get; set; }

        [BsonElement("ramAmount")]
        public int RamAmount { get; set; }

        [BsonElement("user"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string User { get; set; }
    }
}
