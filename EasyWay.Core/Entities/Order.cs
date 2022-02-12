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
    [BsonIgnoreExtraElements]
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
        public bool DoneOrNot { get; set; }
        [JsonProperty("name")]
        [BsonElement("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        [BsonElement("email")]
        public string Email { get; set; }
        [JsonProperty("phone")]
        [BsonElement("phone")]
        public string Phone { get; set; }

        [JsonProperty("lat")]
        [BsonElement("lat")]
        public double Lat { get; set; }
        [JsonProperty("lng")]
        [BsonElement("lng")]
        public double Lng { get; set; }
        [JsonProperty("addressLine")]
        [BsonElement("addressLine")]
        public string AddressLine { get; set; }
        //
        [JsonProperty("deliverymanNum")]
        [BsonElement("deliverymanNum")]
        public int DeliverymanNum { get; set; }
        //[JsonProperty("deliverymanName")]
        //[BsonElement("deliverymanName")]
        //public string DeliverymanName { get; set; }
        public DeliveryMan deliveryMan;


        //public void SetAddress(double lon, double lat)
        //{
        //    Address = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(
        //          new GeoJson2DGeographicCoordinates(lon, lat));
        //}
        public void SetDeliverymanId(string id)
        {
          
            this.DeliverymanId = id;
        }
        public void SetDeliverymanNum(int num)
        {

            this.DeliverymanNum = num;
        }
        //public void SetDeliverymanName(string name)
        //{

        //    this.DeliverymanName = name;
        //}

    }


}
