using Lab8.DAL;
using Lab8.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab8.Controllers
{
    public class TypeTransportsController : Controller
    {
        private readonly TripContext _context;

        public TypeTransportsController(TripContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("typesTransport/get")]
        public IEnumerable<TypeTransport> Index()
        {
            return _context.TypesTransport.ToList();
        }

        [HttpGet]
        [Route("typesTransport/get/{id}")]
        public IActionResult Details(int id)
        {
            if (_context.TypesTransport == null)
            {
                return NotFound();
            }

            var type = _context.TypesTransport
                .FirstOrDefault(t => t.IdTypeTransport == id);
            if (type == null)
            {
                return NotFound();
            }

            return Ok(type);
        }

        [HttpPut]
        [Route("typesTransport/edit/{id}")]
        public IActionResult Edit(int id, string name)
        {
            if (_context.TypesTransport == null)
            {
                return NotFound();
            }

            var type = _context.TypesTransport.Find(id);
            if (type == null)
            {
                return NotFound();
            }

            type.Name = name;

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("typesTransport/delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (_context.TypesTransport == null)
                return NotFound();

            var type = _context.TypesTransport.Find(id);
            if (type == null)
                return NotFound();

            _context.TypesTransport.Remove(type);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("typesTransport/create")]
        public IActionResult Create(string name)
        {
            if (_context.TypesTransport == null)
                return NotFound();

            _context.TypesTransport.Add(new TypeTransport
            {
                Name = name
            });

            _context.SaveChanges();
            return Ok();
        }
    }
}
