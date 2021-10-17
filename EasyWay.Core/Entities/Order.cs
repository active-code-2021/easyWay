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

    public abstract class JsonCreationConverter<T> : JsonConverter
    {
        /// <summary>
        /// Create an instance of objectType, based properties in the JSON object
        /// </summary>
        /// <param name="objectType">type of object expected</param>
        /// <param name="jObject">
        /// contents of JSON object that will be deserialized
        /// </param>
        /// <returns></returns>
        protected abstract T Create(Type objectType, JObject jObject);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override object ReadJson(MongoDB.Bson.IO.JsonReader reader,
                                        Type objectType,
                                         object existingValue,
                                         System.Text.Json.JsonSerializer serializer)
        {
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            // Create target object based on JObject
            T target = Create(objectType, jObject);

            // Populate the object properties
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }
    }
}
