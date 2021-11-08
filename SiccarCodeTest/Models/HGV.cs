using SiccarCodeTest.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiccarCodeTest.Domain
{
    public class HGV : Vehicle
    {
        public override string Type { get; init; } = VehicleType.HGV;
        public int MaxTrailerLoad { get; init; }
        // VFD ADDED
        public HGV()
            : base()
        {
            this.MaxTrailerLoad = 0;
        }
        public HGV (string _reg, int _nm, Dictionary<string, object> _udf)
            : base(_reg, _udf)
        {
            this.MaxTrailerLoad = _nm;
        }
        // VFD ADDED END
    }
}
