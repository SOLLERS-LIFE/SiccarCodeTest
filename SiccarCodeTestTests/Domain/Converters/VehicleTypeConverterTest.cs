using SiccarCodeTest.Domain;
using SiccarCodeTest.Domain.Converters;
using SiccarCodeTest.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace SiccarCodeTestTests.Domain.Converters
{
    public class VehicleTypeConverterTest
    {
        private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

        [Fact]
        public void Should_Deserialize_HGV()
        {
            // Arrange
            var json = "{\"type\":\"HGV\",\"registration\":\"ab12 12cd\",\"maxTrailerLoad\":\"400\"}";
            var target = new VehicleTypeConverter();

            // Act
            Vehicle result = null;
            var utf8JsonBytes = Encoding.UTF8.GetBytes(json);
            var reader = new Utf8JsonReader(utf8JsonBytes, true, new JsonReaderState());
            while (reader.Read())
            {
                result = target.Read(ref reader, typeof(Vehicle), _serializerOptions);
            }

            // Assert
            Assert.IsType<HGV>(result);

            var typeResult = result as HGV;
            Assert.Equal("ab12 12cd", typeResult?.Registration);
            Assert.Equal(VehicleType.HGV, typeResult?.Type);
            Assert.Equal(400, typeResult?.MaxTrailerLoad);
        }

        [Fact]
        public void Should_Deserialize_Car()
        {
            // Arrange
            var json = "{\"type\":\"Car\",\"registration\":\"ab12 12cd\",\"numberOfSeats\":\"2\"}";
            var target = new VehicleTypeConverter();

            // Act
            Vehicle result = null;
            var utf8JsonBytes = Encoding.UTF8.GetBytes(json);
            var reader = new Utf8JsonReader(utf8JsonBytes, true, new JsonReaderState());
            while (reader.Read())
            {
                result = target.Read(ref reader, typeof(Vehicle), _serializerOptions);
            }

            // Assert
            Assert.IsType<Car>(result);

            var typeResult = result as Car;
            Assert.Equal("ab12 12cd", typeResult?.Registration);
            Assert.Equal(VehicleType.Car, typeResult?.Type);
            Assert.Equal(2, typeResult?.NumberOfSeats);
        }

        [Fact]
        public void Should_Throw_When_UnexpectedType()
        {
            // Arrange
            var json = "{\"type\":\"nonExistentType\"}";
            var target = new VehicleTypeConverter();

            // Act
            var utf8JsonBytes = Encoding.UTF8.GetBytes(json);
            var reader = new Utf8JsonReader(utf8JsonBytes, true, new JsonReaderState());

            try
            {
                while (reader.Read())
                {
                    target.Read(ref reader, typeof(Vehicle), _serializerOptions);
                }
            }
            catch (Exception e)
            {
                // Assert
                Assert.IsType<NotSupportedException>(e);
            }
        }

        [Fact]
        public void Should_Throw_When_Empty()
        {
            // Arrange
            var json = "{}";
            var target = new VehicleTypeConverter();

            // Act
            var utf8JsonBytes = Encoding.UTF8.GetBytes(json);
            var reader = new Utf8JsonReader(utf8JsonBytes, true, new JsonReaderState());

            try
            {
                while (reader.Read())
                {
                    target.Read(ref reader, typeof(Vehicle), _serializerOptions);
                }
            }
            catch (Exception e)
            {
                // Assert
                Assert.IsType<KeyNotFoundException>(e);
            }

        }
    }
}
