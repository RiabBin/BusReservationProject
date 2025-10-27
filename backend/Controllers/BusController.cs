using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BusController(AppDbContext context)
        {
            _context = context;
        }

        // Static demo data
        private static List<Bus> StaticBuses = new List<Bus>
        {
            new Bus { Id = 1, Company = "GreenLine",
             BusName = "GL-101", From = "Dhaka",
              To = "Chittagong", JourneyDate = DateTime.Today,
              StartTime = "08:00 AM", ArrivalTime = "02:00 PM",
               BoardingPoint = "Gabtoli", DroppingPoint = "Dampara",
                BusType = "AC", Fare = 800,
                 TotalSeats = 40, BookedSeats = 10 },

                new Bus { Id = 2, Company = "Hanif", BusName = "HN-202",
                 From = "Dhaka", To = "Chittagong", JourneyDate = DateTime.Today,
                 StartTime = "09:00 AM", ArrivalTime = "02:30 PM", BoardingPoint = "Mohakhali",
               DroppingPoint = "Rajpara", BusType = "Non-AC",
                Fare = 600, TotalSeats = 45, BookedSeats = 15 },

                new Bus { Id = 3, Company = "Shohag", BusName = "SH-303",
                 From = "Sylhet", To = "Dhaka", JourneyDate = DateTime.Today,
                 StartTime = "07:30 AM", ArrivalTime = "01:30 PM",
              BoardingPoint = "Kodomtoli", DroppingPoint = "Gabtoli",
                BusType = "AC", Fare = 700, TotalSeats = 42, BookedSeats = 12 },

                new Bus { Id = 4, Company = "Shohag", BusName = "SH-303",
             From = "Sylhet", To = "Dhaka", JourneyDate = DateTime.Today,
             StartTime = "07:30 AM", ArrivalTime = "01:30 PM",
             BoardingPoint = "Kodomtoli", DroppingPoint = "Gabtoli",
             BusType = "AC", Fare = 700, TotalSeats = 42, BookedSeats = 12 },

            new Bus { Id = 5, Company = "Shohag", BusName = "SH-303",
             From = "Sylhet", To = "Dhaka", JourneyDate = DateTime.Today,
             StartTime = "07:30 AM", ArrivalTime = "01:30 PM",
             BoardingPoint = "Kodomtoli", DroppingPoint = "Gabtoli",
             BusType = "AC", Fare = 700, TotalSeats = 42, BookedSeats = 12 },

            new Bus { Id = 6, Company = "Shohag", BusName = "SH-303",
             From = "Sylhet", To = "Chittagong", JourneyDate = DateTime.Today,
             StartTime = "07:30 AM", ArrivalTime = "01:30 PM",
             BoardingPoint = "Kodomtoli", DroppingPoint = "Gabtoli",
             BusType = "AC", Fare = 700, TotalSeats = 42, BookedSeats = 12 }
        };

        [HttpGet("getAllBuses")]
        public async Task<IActionResult> GetAllBuses()
        {
            var dbBuses = await _context.Buses.ToListAsync();

            var allBuses = StaticBuses.Concat(dbBuses).ToList();

            return Ok(allBuses);
        }


        [HttpGet("searchBus")]
public IActionResult SearchBus(string from, string to, DateTime? date)
{

    var staticResult = StaticBuses
        .Where(b => b.From.Equals(from, StringComparison.OrdinalIgnoreCase)
                 && b.To.Equals(to, StringComparison.OrdinalIgnoreCase)
                 && (!date.HasValue || b.JourneyDate.Date == date.Value.Date))
        .ToList();

   
    var dbResult = _context.Buses
        .Where(b => b.From.ToLower() == from.ToLower()
                 && b.To.ToLower() == to.ToLower()
                 && (!date.HasValue || b.JourneyDate.Date == date.Value.Date))
        .ToList();


    var result = staticResult.Concat(dbResult).ToList();

    return Ok(result);
}

        [HttpGet("GetAvailableRoutes")]
        public async Task<IActionResult> GetAvailableRoutes()
        {
            var dbRoutes = await _context.Buses
                .Select(b => new { b.From, b.To })
                .ToListAsync();

            var staticRoutes = StaticBuses
                .Select(b => new { b.From, b.To })
                .ToList();

            var allRoutes = staticRoutes.Concat(dbRoutes)
                .Distinct()
                .ToList();

            return Ok(allRoutes);
        }
        [HttpGet("getBookedSeats")]
        public async Task<IActionResult> GetBookedSeats(int busId)
        {
            var bus = StaticBuses.FirstOrDefault(b => b.Id == busId)
                      ?? await _context.Buses.FirstOrDefaultAsync(b => b.Id == busId);

            if (bus == null)
                return NotFound("Bus not found.");

            var booked = new
            {
                bus.BusName,
                bus.TotalSeats,
                bus.BookedSeats,
                SeatsLeft = bus.TotalSeats - bus.BookedSeats
            };
            return Ok(booked);
        }

        [HttpGet("seats/{busId}")]
        public async Task<IActionResult> GetSeats(int busId)
        {
            var bus = StaticBuses.FirstOrDefault(b => b.Id == busId)
                      ?? await _context.Buses
                          .Include(b => b.Seats)
                          .FirstOrDefaultAsync(b => b.Id == busId);

            if (bus == null)
                return NotFound("Bus not found");

            return Ok(bus.Seats);
        }
    }
}
