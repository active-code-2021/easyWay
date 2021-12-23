using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        [JsonProperty("deliverymanId")]
        [BsonElement("deliverymanId")]
        public string DeliverymanId { get; set; }
        [JsonProperty("doneOrNot")]
        [BsonElement("doneOrNot")]
        public bool DoneOrNot { get; set; } = true;
        [JsonProperty("name")]
        [BsonElement("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        [BsonElement("email")]
        public string Email { get; set; }
        [JsonProperty("phone")]
        [BsonElement("phone")]
        public string Phone { get; set; }
        [JsonProperty("addressLat")]
        [BsonElement("addressLat")]
        public double addressLat { get; set; }
        [JsonProperty("addressLon")]
        [BsonElement("addressLon")]
        public double addressLon { get; set; }

        public DeliveryMan deliveryMan;

        [System.Text.Json.Serialization.JsonIgnore]
        [BsonElement("address")]
        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Address { get; private set; }


        public void SetAddress(double lon, double lat)
        {
            Address = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(
                  new GeoJson2DGeographicCoordinates(lon, lat));
        }
        public void SetDeliverymanId(string id)
        {
          
            this.DeliverymanId = id;
        }

    }


}
