using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace DeviceManagementAPI.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }
        
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("role")]
        public string Role { get; set; }

        [BsonElement("location")]
        public string Location { get; set; }

        [BsonElement("device"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Device { get; set; }
    }
}
