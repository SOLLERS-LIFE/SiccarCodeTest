using SiccarCodeTest.Domain.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SiccarCodeTest.Interfaces.Domain
{
    [JsonConverter(typeof(VehicleTypeConverter))]
    public abstract class Vehicle
    {
        // VFD MODIFIED
        public Vehicle() 
        {
            this.userDefinedProperties = new Dictionary<string, object>();
        }
        // VFD MODIFIED END
        // VFD ADDED
        public Vehicle(string _reg, Dictionary<string, object> _udf)
        {
            this.Registration = _reg;
            this.userDefinedProperties = _udf;
        }
        // VFD ADDED END

        public string Registration { get; init; }
        public virtual string Type { get; init; }
        public int TotalTax { get; private set; } = 0;

        public void SetTotalTax(int tax) => TotalTax = tax;

        // VFD ADDED
        // to keep any user attributes exept of predefined
        public Dictionary<string, object> userDefinedProperties { get; init; }
        // VFD ADDED END
    }
}
