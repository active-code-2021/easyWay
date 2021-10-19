using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonConverter = Newtonsoft.Json.JsonConverter;

namespace EasyWay.Core.Entities
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        //[BsonElement("customerId")]
        //public string CustomerId { get; set; }
        //[BsonElement("deliveryManId")]
        //public string DeliveryManId { get; set; }
        [JsonProperty("doneOrNot")]
        [BsonElement("doneOrNot")]
        public bool DoneOrNot { get; set; } = false;
        [JsonProperty("address")]
        [BsonElement("address")]
        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Address { get; set; }
        [JsonProperty("firstname")]
        [BsonElement("firstname")]
        public string Firstname { get; set; }
        [JsonProperty("lastname")]
        [BsonElement("lastname")]
        public string Lastname { get; set; }
        [JsonProperty("email")]
        [BsonElement("email")]
        public string Email { get; set; }
        [JsonProperty("phone")]
        [BsonElement("phone")]
        public string Phone { get; set; }


    }

 
}
