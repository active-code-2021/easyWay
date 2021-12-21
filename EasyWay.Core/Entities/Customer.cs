using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace EasyWay.Core.Entities
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("firstname")]
        public string Firstname { get; set; }
        [BsonElement("lastname")]
        public string Lastname { get; set; }
        //[BsonElement("lat_lng")]
        //public Address Lat_lng { get; set; }
        //[BsonElement("address")]
        //public GeoJsonPoint<GeoJson2DGeographicCoordinates> Address { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("phone")]
        public string Phone { get; set; }


        public Customer()
        {
                
        }
    }
}
