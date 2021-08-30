using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EasyWay.Core.Entities
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("customerId")]
        public string CustomerId { get; set; }
        [BsonElement("deliveryManId")]
        public string DeliveryManId { get; set; }
    }
}
