using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWay.Core.Entities
{
   public class Address
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("lat")]
        public double Lat { get; set; }
        [BsonElement("lng")]
        public double Lng { get; set; }

        public Address()
        {

        }
    }
}
