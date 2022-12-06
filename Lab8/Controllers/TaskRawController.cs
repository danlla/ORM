using Lab8.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace Lab8.Controllers
{
    public class TaskRawController : Controller
    {
        private readonly MySqlConnection _context;

        public TaskRawController(MySqlConnection context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("raw/task1/{id}")]
        public IActionResult Task1(int id)
        {
            _context.Open();
            using var command = _context.CreateCommand();
            command.CommandText = @"
                            SELECT id_transport, name, title, government_number, max_capacity, low_floor
                            FROM transport
                            INNER JOIN type_transport USING(id_type_transport)
                            INNER JOIN model USING(id_model)
                            WHERE id_transport = @id;";

            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return Ok(new
                {
                    Id = (int)reader.GetValue(0),
                    Name = (string)reader.GetValue(1),
                    Title = (string)reader.GetValue(2),
                    GovernmentNumber = (string)reader.GetValue(3),
                    MaxCapacity = (int)reader.GetValue(4),
                    LowFloor = (ulong)reader.GetValue(5)
                });
            }

            return NotFound();
        }

        [HttpGet]
        [Route("raw/task2")]
        public IActionResult Task2(DateTime start, DateTime end)
        {
            _context.Open();
            using var command = _context.CreateCommand();
            command.CommandText = @"
                            SELECT id_driver, full_name, passport, license, phone_number
                            FROM driver
                            INNER JOIN trip USING(id_driver)
                            INNER JOIN address USING(id_address)
                            WHERE start_date > @start AND end_date < @end
                            ORDER BY full_name;";

            command.Parameters.AddWithValue("@start", start);
            command.Parameters.AddWithValue("@end", end);

            var list = new List<Task2Response>();

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Task2Response
                {
                    Id = (int)reader.GetValue(0),
                    FullName = (string)reader.GetValue(1),
                    Passport = (string)reader.GetValue(2),
                    License = (string)reader.GetValue(3),
                    PhoneNumber = (string)reader.GetValue(4),
                });
            }

            return Ok(list);
        }

        [HttpGet]
        [Route("raw/task3")]
        public IActionResult Task3()
        {
            _context.Open();
            using var command = _context.CreateCommand();
            command.CommandText = @"
                            SELECT title, sum(minute(end_date-start_date)) as minutes
                            FROM transport
                            INNER JOIN trip USING(id_transport)
                            INNER JOIN model USING(id_model)
                            GROUP BY id_model
                            HAVING sum(minute(end_date-start_date));";

            var list = new List<Task3Response>();

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Task3Response
                {
                    Title = (string)reader.GetValue(0),
                    Time = (decimal)reader.GetValue(1),
                });
            }

            return Ok(list);
        }

        [HttpGet]
        [Route("raw/task4")]
        public IActionResult Task4()
        {
            _context.Open();
            using var command = _context.CreateCommand();
            command.CommandText = @"
                            SELECT id_driver, full_name, passport, license, phone_number, count(*) as count
                            FROM driver
                            INNER JOIN trip USING(id_driver)
                            GROUP BY id_driver
                            HAVING count(*)
                            ORDER BY count(*) DESC LIMIT 5;";

            var list = new List<Task4Response>();

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Task4Response
                {
                    Id = (int)reader.GetValue(0),
                    FullName = (string)reader.GetValue(1),
                    Passport = (string)reader.GetValue(2),
                    License = (string)reader.GetValue(3),
                    PhoneNumber = (string)reader.GetValue(4),
                    Count = (long)reader.GetValue(5)
                });
            }

            return Ok(list);
        }

        [HttpGet]
        [Route("raw/task5")]
        public IActionResult Task5()
        {
            _context.Open();
            using var command = _context.CreateCommand();
            command.CommandText = @"
                            SELECT id_driver, full_name, passport, license, phone_number, count(*) as count, avg_time, max_time from driver
                            INNER JOIN trip USING(id_driver)
                            JOIN (
		                            SELECT id_driver, max(timestampdiff(minute, start_date, end_date)) as max_time from driver
		                            INNER JOIN trip USING(id_driver)
		                            GROUP BY id_driver
                            ) as max_times USING(id_driver)
                            JOIN (
		                            SELECT id_driver, avg(timestampdiff(minute, start_date, end_date)) as avg_time from driver
		                            INNER JOIN trip USING(id_driver)
		                            GROUP BY id_driver
                            ) as avg_times USING(id_driver)
                            GROUP BY id_driver;";

            var list = new List<Task5Response>();

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Task5Response
                {
                    Id = (int)reader.GetValue(0),
                    FullName = (string)reader.GetValue(1),
                    Passport = (string)reader.GetValue(2),
                    License = (string)reader.GetValue(3),
                    PhoneNumber = (string)reader.GetValue(4),
                    Count = (long)reader.GetValue(5),
                    Avg = (decimal)reader.GetValue(6),
                    Max = (long)reader.GetValue(7)
                });
            }

            return Ok(list);
        }

        [HttpGet]
        [Route("raw/task6")]
        public IActionResult Task6(DateTime start, DateTime end)
        {
            _context.Open();
            using var command = _context.CreateCommand();
            command.CommandText = @"
                            SELECT count(*) as count, id_transport, government_number, title, max_capacity from transport
                            INNER JOIN trip USING(id_transport)
                            INNER JOIN model USING(id_model)
                            WHERE start_date>@start and end_date<@end
                            GROUP BY id_transport
                            HAVING count(*) = (SELECT count(*) from trip
                                                WHERE start_date > @start and end_date<@end
                                                GROUP BY id_transport
                                                ORDER BY count(*) DESC LIMIT 1)
                            ORDER BY count(*) DESC";

            command.Parameters.AddWithValue("@start", start);
            command.Parameters.AddWithValue("@end", end);

            var list = new List<Task6Response>();

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Task6Response
                {
                    Count = (long)reader.GetValue(0),
                    IdTransport = (int)reader.GetValue(1),
                    GovernmentNumber = (string)reader.GetValue(2),
                    Title = (string)reader.GetValue(3),
                    MaxCapacity = (int)reader.GetValue(4),
                });
            }

            return Ok(list);
        }
    }
}
