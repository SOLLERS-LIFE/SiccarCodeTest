using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiccarCodeTest.Domain
{
    public class TypeMapHolder
    {
        // VFD EXTENDED AND RELOCATED
        /// <summary> Represented all possible car types and their target propertie's names </summary>
        public class vhclDscr
        {
            public Type tp { get; init; }
            public string propertyName { get; init; }
            public string taxRuleName { get; init; }
            public string taxRuleNameAdd { get; init; }
        }
        public static readonly Dictionary<string, vhclDscr> TypeMap = new Dictionary<string, vhclDscr>(StringComparer.OrdinalIgnoreCase)
        {
            { VehicleType.Car, new vhclDscr {tp = typeof(Car), propertyName = "NumberOfSeats", taxRuleName = "CarBaseTax", taxRuleNameAdd = "NumberOfCarSeats"} },
            { VehicleType.HGV, new vhclDscr {tp = typeof(HGV), propertyName = "MaxTrailerLoad", taxRuleName = "HGVBaseTax", taxRuleNameAdd = "MaxHGVTrailerLoad"} }
        };
        // VFD EXTENDED RELOCATED END
    }
}
