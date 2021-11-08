using SiccarCodeTest.Domain;
using SiccarCodeTest.Domain.Interfaces;
using SiccarCodeTest.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace SiccarCodeTest.Services
{
    public static class TaxCalculator
    {
        /* VFD REMOVED
        private static readonly List<IVehicleTaxRule> _rules = new List<IVehicleTaxRule>
        {
            VehicleTaxRule.HGVBaseTax,
            VehicleTaxRule.CarBaseTax,
            VehicleTaxRuleWithCondition.MaxHGVTrailerLoad,
            VehicleTaxRuleWithCondition.NumberOfCarSeats,
        };
        VFD REMOVED END */

        /// <summary>
        /// Calculates the total tax for a vehicle using a list of tax rules.
        /// </summary>
        /// <param name="vehicle"></param>
        public static void CalulateVehicleTax(Vehicle vehicle)
        {
            // VFD ADDED
            _ = vehicle ?? throw new ArgumentNullException(nameof(vehicle), "Vehicle cannot be null.");

            if (TypeMapHolder.TypeMap.TryGetValue(vehicle.Type, out TypeMapHolder.vhclDscr _tpd))
            {
                FieldInfo refTaxRule = typeof(VehicleTaxRule).GetField(_tpd.taxRuleName, BindingFlags.Public | BindingFlags.Static);
                FieldInfo refTaxRuleAdd = typeof(VehicleTaxRuleWithCondition).GetField(_tpd.taxRuleNameAdd, BindingFlags.Public | BindingFlags.Static);

                vehicle.SetTotalTax(
                                    ((VehicleTaxRule)(refTaxRule.GetValue(null))).CalculateTax(vehicle)
                                    +
                                    ((VehicleTaxRule)(refTaxRuleAdd.GetValue(null))).CalculateTax(vehicle)
                                   );
            }
            else
            {
                throw new Exception($"type property should be {VehicleType.Car} or {VehicleType.HGV}");
            }
            // VFD ADDED END
        }
    }
}

