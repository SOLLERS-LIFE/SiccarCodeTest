using SiccarCodeTest.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SiccarCodeTest.Domain.Converters
{
    public class VehicleTypeConverter : JsonConverter<Vehicle>
    {
        /// <summary>
        /// The logic for determining which child type a vehicle should be deserialised into. 
        /// </summary>
        public override Vehicle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            //TODO
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Vehicle value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }

        private static readonly Dictionary<string, Type> TypeMap = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
        {
            { VehicleType.Car, typeof(Car) },
            { VehicleType.HGV, typeof(HGV) }
        };
    }
}
