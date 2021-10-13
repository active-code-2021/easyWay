using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

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
        [BsonElement("doneOrNot")]
        public bool DoneOrNot { get; set; } = false;
        [BsonElement("address")]
        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Address { get; set; }
    }
}
