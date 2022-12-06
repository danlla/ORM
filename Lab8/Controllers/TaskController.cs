using Lab8.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Lab8.Controllers
{
    public class TaskController : Controller
    {
        private readonly TripContext _context;

        public TaskController(TripContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("task1/{id}")]
        public IActionResult Task1(int id)
        {
            if (_context.Transports == null)
            {
                return NotFound();
            }

            var transport = from t in _context.Transports
                            join type in _context.TypesTransport on t.TransportTypeId equals type.IdTypeTransport
                            join m in _context.Models on t.ModelId equals m.IdModel
                            where t.IdTransport == id
                            select new { t.GovernmentNumber, type.Name, m.Title, m.LowFloor, m.MaxCapacity };

            if (transport == null)
            {
                return NotFound();
            }

            return Ok(transport);
        }

        [HttpGet]
        [Route("task2")]
        public IActionResult Task2(DateTime start, DateTime end)
        {
            if (_context.Transports == null)
            {
                return NotFound();
            }

            var drivers = from d in _context.Drivers
                          join t in _context.Trips on d.IdDriver equals t.DriverId
                          join a in _context.Addresses on d.AddressID equals a.IdAddress
                          where t.StartDate > start && t.EndDate < end
                          orderby d.FullName
                          select new { d.FullName, d.Passport, d.License, d.PhoneNumber, a.City, a.Street, a.House, t.StartDate, t.EndDate };

            if (drivers == null)
            {
                return NotFound();
            }

            return Ok(drivers);
        }

        [HttpGet]
        [Route("task3")]
        public IActionResult Task3()
        {
            if (_context.Transports == null || _context.Trips == null || _context.Models == null)
            {
                return NotFound();
            }

            var sumTime = from t in _context.Transports
                          join tr in _context.Trips on t.IdTransport equals tr.TransportId
                          join m in _context.Models on t.ModelId equals m.IdModel
                          group new { t.ModelId, Time = tr.EndDate - tr.StartDate } by m.IdModel into tmp
                          orderby tmp.Key
                          select new { tmp.Key, Time = tmp.Sum(i => i.Time.Seconds + i.Time.Minutes * 60 + i.Time.Hours * 3600) / (double)3600 };

            if (sumTime == null)
            {
                return NotFound();
            }

            return Ok(sumTime);
        }

        [HttpGet]
        [Route("task4")]
        public IActionResult Task4()
        {
            if (_context.Drivers == null || _context.Trips == null)
            {
                return NotFound();
            }

            var topDrivers = from d in _context.Drivers
                             join tr in _context.Trips on d.IdDriver equals tr.DriverId
                             group new { d.IdDriver, d.FullName, d.Passport, d.License, d.PhoneNumber } by d.IdDriver into tmp
                             orderby tmp.Count() descending
                             select new { Id = tmp.Key, tmp.First().FullName, tmp.First().Passport, tmp.First().License, tmp.First().PhoneNumber, Count = tmp.Count() };

            if (topDrivers == null)
            {
                return NotFound();
            }

            return Ok(topDrivers.Take(5));
        }

        [HttpGet]
        [Route("task5")]
        public IActionResult Task5()
        {
            if (_context.Drivers == null || _context.Trips == null)
            {
                return NotFound();
            }

            var drivers = from tr in _context.Trips
                          join d in _context.Drivers on tr.DriverId equals d.IdDriver
                          group new { d.IdDriver, d.FullName, d.Passport, d.License, d.PhoneNumber, Time = tr.EndDate - tr.StartDate } by d.IdDriver into tmp
                          orderby tmp.Count() descending
                          select new
                          {
                              Id = tmp.Key,
                              tmp.First().FullName,
                              tmp.First().Passport,
                              tmp.First().License,
                              tmp.First().PhoneNumber,
                              Count = tmp.Count(),
                              Max = tmp.Max(i => i.Time.Seconds + i.Time.Minutes * 60 + i.Time.Hours * 3600) / (double)3600,
                              Avg = tmp.Average(i => i.Time.Seconds + i.Time.Minutes * 60 + i.Time.Hours * 3600) / (double)3600
                          };

            if (drivers == null)
            {
                return NotFound();
            }

            return Ok(drivers);
        }

        [HttpGet]
        [Route("task6")]
        public IActionResult Task6(DateTime start, DateTime end)
        {
            if (_context.Drivers == null)
            {
                return NotFound();
            }

            var drivers = from t in _context.Transports
                          join tr in _context.Trips on t.IdTransport equals tr.TransportId
                          where tr.StartDate > start && tr.EndDate < end
                          group new { t.IdTransport, t.GovernmentNumber, t.YearOFManufacture } by t.IdTransport into transport
                          where transport.Count() ==
                          (from t in _context.Transports
                           join tr in _context.Trips on t.IdTransport equals tr.TransportId
                           where tr.StartDate > start && tr.EndDate < end
                           group new { t.IdTransport, Start = tr.StartDate, End = tr.EndDate } by t.IdTransport into tmp
                           orderby tmp.Count() descending
                           select tmp.Count()).First()
                          select new { Count = transport.Count(), transport.First().IdTransport, transport.First().GovernmentNumber };

            if (drivers == null)
            {
                return NotFound();
            }

            return Ok(drivers);
        }
    }
}
