using SiccarCodeTest.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
// VFD ADDED
using SiccarCodeTest.Domain;
// VFD ADDED END

namespace SiccarCodeTest.Domain.Converters
{
    public class VehicleTypeConverter : JsonConverter<Vehicle>
    {
        // VFD ADDED 
        /// <summary>
        /// the dictionary of all properties and their values provided by user 
        /// </summary>
        private Dictionary<string, object> _attributes { get; set; }
        // VFD ADDED END
        /// <summary>
        /// The logic for determining which child type a vehicle should be deserialised into. 
        /// </summary>
        public override Vehicle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // VFD ADDED
            _ = typeToConvert ?? throw new ArgumentNullException(nameof(typeToConvert), "typeToConvert cannot be null.");

            // Properties can go in any sequence, as such - build directory first
            _attributes = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Cannot find Vehicle information");
            }
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read();

                    int iVal = 0;
                    switch (reader.TokenType)
                    {
                        case JsonTokenType.String:
                            _attributes.Add(propertyName, reader.GetString());
                            break;
                        case JsonTokenType.Number:
                            if (!reader.TryGetInt32(out iVal))
                            {
                                throw new NotSupportedException("Property " + $"{propertyName}: "+"Numeric can not be converted to Int32");
                            }
                            else
                            {
                                _attributes.Add(propertyName, iVal);
                            }
                            break;
                        default:
                            throw new NotSupportedException("Property "+$"{propertyName}");
                    }
                }
            }
            if (_attributes.Count == 0)
            {
                throw new KeyNotFoundException("Cannot find Vehicle information");
            }
            // Generate necessary class
            return buildVehicle();
        }
        // VFD ADDED END
        // VFD ADDED
        /// <summary> Building on vehicle </summary>
        /// <param name="reg">registration info of a vehicle</param>
        /// <param name="vtp">type name of a vehicle</param>
        /// <returns>The vehicle builded</returns>
        private Vehicle buildVehicle()
        {
            if (TypeMapHolder.TypeMap.TryGetValue(getInputAttributeString("type"), out TypeMapHolder.vhclDscr _tpd))
            {
                return (Vehicle)Activator.CreateInstance(_tpd.tp,
                                                         getInputAttributeString("registration"),
                                                         getInputAttributeInt(_tpd.propertyName),
                                                         _attributes);
            }
            else
            {
                throw new NotSupportedException($"type property should be {VehicleType.Car} or {VehicleType.HGV}");
            }
        }
        /// <summary> Get string value of property by it's name </summary>
        /// <param name="attrName">name of a property</param>
        /// <returns>String, representing property value</returns>
        private string getInputAttributeString(string attrName)
        {
            _ = attrName ?? throw new ArgumentNullException(nameof(attrName), "Property name cannot be null.");

            if (_attributes.TryGetValue(attrName, out object tp))
            {
                switch (Type.GetTypeCode(tp.GetType()))
                {
                    case TypeCode.String:
                        _attributes.Remove(attrName);
                        return (string)tp;
                    default:
                        throw new JsonException($"{attrName} property should be a string");
                }
            }
            else
            {
                throw new JsonException($"Vehicle information does not contain {attrName} property");
            }
        }
        /// <summary> Get int value of property by it's name </summary>
        /// <param name="attrName">name of a property</param>
        /// <returns>integer, representing property value</returns>
        private int getInputAttributeInt(string attrName)
        {
            _ = attrName ?? throw new ArgumentNullException(nameof(attrName), "Property name cannot be null.");

            if (_attributes.TryGetValue(attrName, out object tp))
            {
                switch (Type.GetTypeCode(tp.GetType()))
                {
                    case TypeCode.Int32:
                        _attributes.Remove(attrName);
                        return (int)tp;
                    case TypeCode.String:
                        if (int.TryParse((string)tp,out int iVal))
                        {
                            _attributes.Remove(attrName);
                            return iVal;
                        }
                        else
                        {
                            throw new JsonException($"{attrName} property should be convertable to a number");
                        }
                    default:
                        throw new JsonException($"{attrName} property should be a number");
                }
            }
            else
            {
                throw new JsonException($"Vehicle information does not contain {attrName} property");
            }
        }
        // VFD ADDED END


        public override void Write(Utf8JsonWriter writer, Vehicle value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
