using SiccarCodeTest.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiccarCodeTest.Domain
{
    public class Car : Vehicle
    {
        public override string Type { get; init; } = VehicleType.Car;
        public int NumberOfSeats { get; init; }
        // VFD ADDED
        public Car()
            : base()
        {
            this.NumberOfSeats = 0;
            
        }
        public Car (string _reg, int _nm, Dictionary<string, object> _udf)
            : base (_reg, _udf)
        {
            this.NumberOfSeats = _nm;
        }
        // VFD ADDED END
    }
}
