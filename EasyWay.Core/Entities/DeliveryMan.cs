using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EasyWay.Core.Entities
{
   public class DeliveryMan
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
       
        public string Id { get; set; }
        [BsonElement("firstName")]
        public string FirstName { get; set; }
        [BsonElement("lastName")]
        public string LastName { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }

        //[BsonElement("vehicleType")]
        //public string VehicleType{ get; set; }

        [BsonElement("active")]
        public bool Active { get; set; } = true;

        [BsonElement("phone")]
        public string Phone { get; set; }
        [BsonElement("tz")]
        public string Tz { get; set; }

    }
}
