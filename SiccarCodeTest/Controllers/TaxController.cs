using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiccarCodeTest.Interfaces.Domain;
using SiccarCodeTest.Repositories;
using SiccarCodeTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// VFD ADDED
using SiccarCodeTest.Domain;
// VFD ADDED END

namespace SiccarCodeTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxController : ControllerBase
    {
        private readonly ILogger<TaxController> _logger;
        private readonly IRepository<Vehicle> _repository;

        public TaxController(ILogger<TaxController> logger, IRepository<Vehicle> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // VFD ADDED
        /// <summary>Add one vehicle to in memory repository</summary>
        /// <param name="attrName">vehicle to add</param>
        /// <returns>true if all is OK</returns>
        [NonAction]
        private async Task<bool> addVehicleAsync (Vehicle vehicle)
        {
            _ = vehicle ?? throw new ArgumentNullException(nameof(vehicle), "Vehicle cannot be null.");

            // assign total tax
            TaxCalculator.CalulateVehicleTax(vehicle);

            // add to repository
            await _repository.InsertOrUpdate(vehicle.Registration, vehicle);

            return await Task.FromResult(true);
        }
        // VFD ADDED END
        /// <summary>
        /// Registers a vehicle to the repository
        /// </summary>
        /// <param name="vehicle">The vehicle that will be stored</param>
        /// <returns name="vehicle">Returns the stored vehicle with the vehicles total tax.</returns>
        [HttpPost("register-vehicle")]
        public async Task<Vehicle> RegisterVehicleAsync(Vehicle vehicle)
        {
            // VFD ADDED
            await addVehicleAsync(vehicle);

            return await Task.FromResult(vehicle);
            // VFD ADDED END
        }

        /// <summary>
        /// Registers multiple vehicles to the repository
        /// </summary>
        /// <param name="vehicles">The list of vehicles to store</param>
        /// <returns name="vehicle">Returns the stored vehicles with the vehicles total tax.</returns>
        [HttpPost("register-vehicles")]
        public async Task<List<Vehicle>> RegisterVehiclesAsync(List<Vehicle> vehicles)
        {
            // VFD ADDED
            _ = vehicles ?? throw new ArgumentNullException(nameof(vehicles), "Vehicle cannot be null.");

            foreach (var vcl in vehicles)
            {
                await addVehicleAsync(vcl);
            }
            return await Task.FromResult(vehicles);
            // VFD ADDED END
        }

        /// <summary>
        /// Returns all registered vehicles
        /// </summary>
        [HttpGet]
        public async Task<List<Vehicle>> GetVehiclesAsync()
        {
            // VFD ADDED
            return await _repository.GetAll();
            // VFD ADDED END
        }
    }
}
