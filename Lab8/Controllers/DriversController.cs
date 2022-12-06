using Lab8.DAL;
using Lab8.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab8.Controllers
{
    public class DriversController : Controller
    {
        private readonly TripContext _context;

        public DriversController(TripContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("drivers/get")]
        public IEnumerable<Driver> Index()
        {
            return _context.Drivers.ToList();
        }

        [HttpGet]
        [Route("drivers/get/{id}")]
        public IActionResult Details(int id)
        {
            if (_context.Drivers == null)
            {
                return NotFound();
            }

            var driver = _context.Drivers
                .FirstOrDefault(d => d.IdDriver == id);
            if (driver == null)
            {
                return NotFound();
            }

            return Ok(driver);
        }

        [HttpPut]
        [Route("drivers/edit/{id}")]
        public IActionResult Edit(int id, string fullName, string passport, string license, int addressId, string phoneNumber)
        {
            if (_context.Drivers == null)
            {
                return NotFound();
            }

            var driver = _context.Drivers.Find(id);
            if (driver == null)
            {
                return NotFound();
            }

            driver.FullName = fullName;
            driver.Passport = passport;
            driver.License = license;
            driver.AddressID = addressId;
            driver.PhoneNumber = phoneNumber;

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("drivers/delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (_context.Drivers == null)
                return NotFound();

            var driver = _context.Drivers.Find(id);
            if (driver == null)
                return NotFound();

            _context.Drivers.Remove(driver);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("drivers/create")]
        public IActionResult Create(string fullName, string passport, string license, int addressId, string phoneNumber)
        {
            if (_context.Drivers == null)
                return NotFound();

            _context.Drivers.Add(new Driver
            {
                FullName = fullName,
                Passport = passport,
                License = license,
                AddressID = addressId,
                PhoneNumber = phoneNumber
            });

            _context.SaveChanges();
            return Ok();
        }
    }
}
