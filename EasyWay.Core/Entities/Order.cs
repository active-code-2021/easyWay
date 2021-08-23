using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EasyWay.Core.Entities
{
    class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CustomerId { get; set; }
        public string DeliveryManId { get; set; }
    }
}
