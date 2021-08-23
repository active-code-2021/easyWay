using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EasyWay.Core.Entities
{
    class DeliveryMan
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string VehicleType{ get; set; }

    }
}
