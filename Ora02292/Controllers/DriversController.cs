using System;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace Ora02292.Controllers
{
    [Route("api/[controller]")]
    public class DriversController
    {
        private readonly Ora02292DbContext _dbContext;

        public DriversController(Ora02292DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("addDriverAndCar")]
        public async Task<Guid> AddDriverAndCar()
        {
            var driver = new Driver();
            _dbContext.Drivers.Add(driver);
            await _dbContext.SaveChangesAsync();

            var car = new Car();
            driver.AddCar(car);
            await _dbContext.SaveChangesAsync();

            return driver.Id;
        }

        [HttpPost("removeDriver")]
        public async Task<OkResult> RemoveDriver(Guid driverId)
        {
            var driver = _dbContext.Drivers.First(x => x.Id == driverId);
            _dbContext.Drivers.Remove(driver);
            await _dbContext.SaveChangesAsync();

            return new OkResult();
        }

        [HttpPost("thisWorks")]
        public async Task<OkResult> ThisWorks()
        {
            var driver = new Driver();
            _dbContext.Drivers.Add(driver);
            await _dbContext.SaveChangesAsync();

            var car = new Car();
            driver.AddCar(car);
            await _dbContext.SaveChangesAsync();

            _dbContext.Drivers.Remove(driver);
            await _dbContext.SaveChangesAsync();

            return new OkResult();
        }
    }
}
