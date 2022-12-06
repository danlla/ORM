using Lab8.DAL;
using Lab8.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab8.Controllers
{
    public class TransportsController : Controller
    {
        private readonly TripContext _context;

        public TransportsController(TripContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("transport/get")]
        public IEnumerable<Transport> Index()
        {
            return _context.Transports.ToList();
        }

        [HttpGet]
        [Route("transport/get/{id}")]
        public IActionResult Details(int id)
        {
            if (_context.Transports == null)
            {
                return NotFound();
            }

            var transport = _context.Transports
                .FirstOrDefault(t => t.IdTransport == id);
            if (transport == null)
            {
                return NotFound();
            }

            return Ok(transport);
        }

        [HttpPut]
        [Route("transport/edit/{id}")]
        public IActionResult Edit(int id, string governmentNumber, int transportTypeId, int modelId, DateTime yearOfManufacture)
        {
            if (_context.Transports == null)
            {
                return NotFound();
            }

            var transport = _context.Transports.Find(id);
            if (transport == null)
            {
                return NotFound();
            }

            transport.GovernmentNumber = governmentNumber;
            transport.TransportTypeId = transportTypeId;
            transport.ModelId = modelId;
            transport.YearOFManufacture = yearOfManufacture;

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("transport/delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (_context.Transports == null)
                return NotFound();

            var transport = _context.Transports.Find(id);
            if (transport == null)
                return NotFound();

            _context.Transports.Remove(transport);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("transport/create")]
        public IActionResult Create(string governmentNumber, int transportTypeId, int modelId, DateTime yearOfManufacture)
        {
            if (_context.Transports == null)
                return NotFound();

            _context.Transports.Add(new Transport
            {
                GovernmentNumber = governmentNumber,
                TransportTypeId = transportTypeId,
                ModelId = modelId,
                YearOFManufacture = yearOfManufacture
            });

            _context.SaveChanges();
            return Ok();
        }
    }
}
