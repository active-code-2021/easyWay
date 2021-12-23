using MongoDB.Driver.GeoJsonObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWay.Api
{
    public class Converter : JsonConverter
    {

        public Converter() { }

        public override bool CanConvert(Type objectType)
        {
            return typeof(GeoJson2DGeographicCoordinates).IsAssignableFrom(objectType);
        }

        public override bool CanWrite
        {
            get { return false; }
        }


        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);

            // Create target object based on JObject
           // GeoJson2DGeographicCoordinates target = new GeoJson2DGeographicCoordinates(jObject.longitude, jObject.latitude);
           var target = new GeoJson2DGeographicCoordinates(1, 2);

            // Populate the object properties
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }
    }
}
