using SiccarCodeTest.Domain;
using SiccarCodeTest.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SiccarCodeTestTests.Domain
{
    public class VehicleTaxRuleTests
    {
        private IVehicleTaxRule _underTest;

        public class CalculateTax : VehicleTaxRuleTests
        {
            public CalculateTax()
            {
                _underTest = new VehicleTaxRule() { AppliesTo = VehicleType.Car, TaxAmount = 100 };
            }

            [Fact]
            public void Should_Throw_When_VehicleIsNull()
            {
                Assert.Throws<ArgumentNullException>(() => _underTest.CalculateTax(null));
            }

            [Fact]
            public void Should_ApplyTax_ToCorrectVehicleType()
            {
                var expected = 100;
                _underTest = new VehicleTaxRule() { AppliesTo = VehicleType.Car, TaxAmount = expected };
                var vehicle = new Car() { Registration = "ab22 bd53" };

                var result = _underTest.CalculateTax(vehicle);

                Assert.Equal(expected, result);
            }

            [Fact]
            public void Should_ApplyZeroTax_When_RuleDoesNotApply()
            {
                var expected = 0;
                _underTest = new VehicleTaxRule() { AppliesTo = VehicleType.Car, TaxAmount = 100 };
                var vehicle = new HGV() { Registration = "ab22 bd53" };

                var result = _underTest.CalculateTax(vehicle);

                Assert.Equal(expected, result);
            }
        }
        public class CarBaseTax : VehicleTaxRule
        {
            [Fact]
            public void Should_ApplyBaseTax()
            {
                var vehicle = new Car() { Registration = "ab22 bd53" };

                var result = CarBaseTax.CalculateTax(vehicle);

                Assert.Equal(100, result);
            }
        }

        public class HGVBaseTax : VehicleTaxRule
        {
            [Fact]
            public void Should_ApplyTax()
            {
                var vehicle = new HGV() { Registration = "ab22 bd53"};

                var result = HGVBaseTax.CalculateTax(vehicle);

                Assert.Equal(140, result);
            }
        }
    }
}
