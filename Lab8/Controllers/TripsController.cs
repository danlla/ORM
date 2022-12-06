using Lab8.DAL;
using Lab8.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab8.Controllers
{
    public class TripsController : Controller
    {
        private readonly TripContext _context;

        public TripsController(TripContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("trips/get")]
        public IEnumerable<Trip> Index()
        {
            return _context.Trips.ToList();
        }

        [HttpGet]
        [Route("trips/get/{id}")]
        public IActionResult Details(int id)
        {
            if (_context.Trips == null)
            {
                return NotFound();
            }

            var trip = _context.Trips
                .FirstOrDefault(t => t.IdTrip == id);
            if (trip == null)
            {
                return NotFound();
            }

            return Ok(trip);
        }

        [HttpPut]
        [Route("trips/edit/{id}")]
        public IActionResult Edit(int id, int driverId, int route, int transportId, DateTime startDate, DateTime endDate)
        {
            if (_context.Trips == null)
            {
                return NotFound();
            }

            var trip = _context.Trips.Find(id);
            if (trip == null)
            {
                return NotFound();
            }

            trip.DriverId = driverId;
            trip.Route = route;
            trip.TransportId = transportId;
            trip.StartDate = startDate;
            trip.EndDate = endDate;

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("trips/delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (_context.Trips == null)
                return NotFound();

            var trip = _context.Trips.Find(id);
            if (trip == null)
                return NotFound();

            _context.Trips.Remove(trip);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("trips/create")]
        public IActionResult Create(int driverId, int route, int transportId, DateTime startDate, DateTime endDate)
        {
            if (_context.Trips == null)
                return NotFound();

            _context.Trips.Add(new Trip
            {
                DriverId = driverId,
                Route = route,
                TransportId = transportId,
                StartDate = startDate,
                EndDate = endDate
            });

            _context.SaveChanges();
            return Ok();
        }
    }
}
