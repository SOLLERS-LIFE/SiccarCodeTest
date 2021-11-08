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
    public class VehicleTaxRuleWithConditionTests
    {
        private IVehicleTaxRule _underTest;

        public class CalculateTax : VehicleTaxRuleWithConditionTests
        {
            public CalculateTax()
            {
                _underTest = new VehicleTaxRuleWithCondition() { AppliesTo = VehicleType.Car, TaxAmount = 100 };
            }

            [Fact]
            public void Should_Throw_When_VehicleIsNull()
            {
                Assert.Throws<ArgumentNullException>(() => _underTest.CalculateTax(null));
            }

            [Fact]
            public void Should_Throw_When_RulePropertyIsNull()
            {
                _underTest = new VehicleTaxRuleWithCondition() { AppliesTo = VehicleType.Car, TaxAmount = 0, Condition = (param) => false };

                var exception = Assert.Throws<ArgumentNullException>(() => _underTest.CalculateTax(new Car()));

                Assert.Equal("PropertyName", exception.ParamName);
            }

            [Fact]
            public void Should_Throw_When_RuleConditionIsNull()
            {
                _underTest = new VehicleTaxRuleWithCondition() { AppliesTo = VehicleType.Car, TaxAmount = 0, PropertyName = "some-property" };

                var exception = Assert.Throws<ArgumentNullException>(() => _underTest.CalculateTax(new Car()));

                Assert.Equal("Condition", exception.ParamName);
            }

            [Fact]
            public void Should_ApplyTax_ToCorrectVehicleType_WithCondition()
            {
                var expected = 100;
                var vehicle = new Car() { Registration = "ab22 bd53", NumberOfSeats = 3 };
                _underTest = new VehicleTaxRuleWithCondition()
                {
                    AppliesTo = VehicleType.Car,
                    TaxAmount = expected,
                    PropertyName = nameof(Car.NumberOfSeats),
                    Condition = numberOfSeats => (int)numberOfSeats > 2
                };

                var result = _underTest.CalculateTax(vehicle);

                Assert.Equal(expected, result);
            }

            [Fact]
            public void Should_Not_ApplyTax_When_CondtitionNotMet()
            {
                var expected = 0;
                var vehicle = new Car() { Registration = "ab22 bd53", NumberOfSeats = 2 };
                _underTest = new VehicleTaxRuleWithCondition()
                {
                    AppliesTo = VehicleType.Car,
                    TaxAmount = 100,
                    PropertyName = nameof(Car.NumberOfSeats),
                    Condition = numberOfSeats => (int)numberOfSeats > 2
                };

                var result = _underTest.CalculateTax(vehicle);

                Assert.Equal(expected, result);
            }

            [Fact]
            public void Should_ApplyZeroTax_When_RuleDoesNotApply()
            {
                var expected = 0;
                var vehicle = new HGV() { Registration = "ab22 bd53" };
                _underTest = new VehicleTaxRuleWithCondition()
                {
                    AppliesTo = VehicleType.Car,
                    TaxAmount = 100,
                    PropertyName = nameof(Car.NumberOfSeats),
                    Condition = numberOfSeats => (int)numberOfSeats > 2
                };

                var result = _underTest.CalculateTax(vehicle);

                Assert.Equal(expected, result);
            }
        }

        public class NumberOfCarSeats : VehicleTaxRuleWithConditionTests
        {
            [Theory]
            [InlineData(2)]
            [InlineData(6)]
            [InlineData(10)]
            public void Should_ApplyTax(int numberOfSeats)
            {
                var vehicle = new Car() { Registration = "ab22 bd53", NumberOfSeats = numberOfSeats };

                var result = VehicleTaxRuleWithCondition.NumberOfCarSeats.CalculateTax(vehicle);

                Assert.Equal(25, result);
            }

            [Theory]
            [InlineData(3)]
            [InlineData(4)]
            [InlineData(5)]
            public void Should_Not_ApplyTax(int numberOfSeats)
            {
                var vehicle = new Car() { Registration = "ab22 bd53", NumberOfSeats = numberOfSeats };

                var result = VehicleTaxRuleWithCondition.NumberOfCarSeats.CalculateTax(vehicle);

                Assert.Equal(0, result);
            }
        }
        public class MaxHGVTrailerLoad : VehicleTaxRuleWithConditionTests
        {
            [Theory]
            [InlineData(201)]
            [InlineData(300)]
            public void Should_ApplyTax(int maxTrailerLoad)
            {
                var vehicle = new HGV() { Registration = "ab22 bd53", MaxTrailerLoad = maxTrailerLoad };

                var result = VehicleTaxRuleWithCondition.MaxHGVTrailerLoad.CalculateTax(vehicle);

                Assert.Equal(100, result);
            }

            [Theory]
            [InlineData(100)]
            [InlineData(200)]
            public void Should_Not_ApplyTax(int maxTrailerLoad)
            {
                var vehicle = new HGV() { Registration = "ab22 bd53", MaxTrailerLoad = maxTrailerLoad };

                var result = VehicleTaxRuleWithCondition.MaxHGVTrailerLoad.CalculateTax(vehicle);

                Assert.Equal(0, result);
            }
        }
    }
}
