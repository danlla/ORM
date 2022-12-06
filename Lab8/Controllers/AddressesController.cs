using Lab8.DAL;
using Lab8.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab8.Controllers
{
    public class AddressesController : Controller
    {
        private readonly TripContext _context;

        public AddressesController(TripContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("address/get")]
        public IEnumerable<Address> Index()
        {
            return _context.Addresses.ToList();
        }

        [HttpGet]
        [Route("address/get/{id}")]
        public IActionResult Details(int id)
        {
            if (_context.Addresses == null)
            {
                return NotFound();
            }

            var address = _context.Addresses
                .FirstOrDefault(a => a.IdAddress == id);
            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        [HttpPut]
        [Route("address/edit/{id}")]
        public IActionResult Edit(int id, string city, string street, string house)
        {
            if (_context.Addresses == null)
            {
                return NotFound();
            }

            var address = _context.Addresses.Find(id);
            if (address == null)
            {
                return NotFound();
            }

            address.City = city;
            address.Street = street;
            address.House = house;

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("address/delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (_context.Addresses == null)
                return NotFound();

            var address = _context.Addresses.Find(id);
            if (address == null)
                return NotFound();

            _context.Addresses.Remove(address);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("address/create")]
        public IActionResult Create(string city, string street, string house)
        {
            if (_context.Addresses == null)
                return NotFound();

            _context.Addresses.Add(new Address
            {
                City = city,
                Street = street,
                House = house
            });

            _context.SaveChanges();
            return Ok();
        }
    }
}
