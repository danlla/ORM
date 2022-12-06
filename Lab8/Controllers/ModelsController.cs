using Lab8.DAL;
using Lab8.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab8.Controllers
{
    public class ModelsController : Controller
    {
        private readonly TripContext _context;

        public ModelsController(TripContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("models/get")]
        public IEnumerable<Model> Index()
        {
            return _context.Models.ToList();
        }

        [HttpGet]
        [Route("models/get/{id}")]
        public IActionResult Details(int id)
        {
            if (_context.Models == null)
            {
                return NotFound();
            }

            var model = _context.Models
                .FirstOrDefault(m => m.IdModel == id);
            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPut]
        [Route("models/edit/{id}")]
        public IActionResult Edit(int id, string title, bool lowFloor, int maxCapacity)
        {
            if (_context.Models == null)
            {
                return NotFound();
            }

            var model = _context.Models.Find(id);
            if (model == null)
            {
                return NotFound();
            }

            model.Title = title;
            model.LowFloor = lowFloor;
            model.MaxCapacity = maxCapacity;

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("models/delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (_context.Models == null)
                return NotFound();

            var model = _context.Models.Find(id);
            if (model == null)
                return NotFound();

            _context.Models.Remove(model);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("models/create")]
        public IActionResult Create(string title, bool lowFloor, int maxCapacity)
        {
            if (_context.Models == null)
                return NotFound();

            _context.Models.Add(new Model { Title = title, LowFloor = lowFloor, MaxCapacity = maxCapacity });

            _context.SaveChanges();
            return Ok();
        }
    }
}
