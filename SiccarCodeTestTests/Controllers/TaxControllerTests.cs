using FakeItEasy;
using Microsoft.Extensions.Logging;
using SiccarCodeTest.Controllers;
using SiccarCodeTest.Domain;
using SiccarCodeTest.Interfaces.Domain;
using SiccarCodeTest.Repositories;
using SiccarCodeTest.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SiccarCodeTestTests.Controllers
{
    public class TaxControllerTests
    {
        private readonly ILogger<TaxController> _fakeLogger;
        private readonly IRepository<Vehicle> _fakeRepository;
        private readonly TaxController _underTest;

        public TaxControllerTests()
        {
            _fakeLogger = A.Fake<ILogger<TaxController>>();
            _fakeRepository = A.Fake<IRepository<Vehicle>>();
            _underTest = new TaxController(_fakeLogger, _fakeRepository);
        }
        [Fact]
        public async Task ShouldReturnCarTax_When_VehicleIsTypeCarAsync()
        {
            var car = new Car { Type = VehicleType.Car };
            
            var response = await _underTest.RegisterVehicleAsync(car);

            Assert.Equal(car, response);
        }
    }
}
