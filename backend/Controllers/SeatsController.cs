using Microsoft.AspNetCore.Mvc;
using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeatsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SeatsController(AppDbContext context)
        {
            _context = context;
        }
        private static readonly List<Seat> StaticSeats = new List<Seat>
        {
            new Seat { Id = 1, BusId = 3, SeatNo = "A1", IsBooked = true },
            new Seat { Id = 2, BusId = 3, SeatNo = "A2", IsBooked = true },
            new Seat { Id = 3, BusId = 1, SeatNo = "A3", IsBooked = false },
            new Seat { Id = 4, BusId = 2, SeatNo = "B1", IsBooked = false },
            new Seat { Id = 5, BusId = 2, SeatNo = "B2", IsBooked = false },
            new Seat { Id = 6, BusId = 2, SeatNo = "B3", IsBooked = false },
            new Seat { Id = 7, BusId = 2, SeatNo = "B4", IsBooked = true }
        };

       [HttpGet("bus/{busId}")]
    public async Task<IActionResult> GetSeatsByBus(int busId)
{
    var dbSeats = await _context.Seats.Where(s => s.BusId == busId).ToListAsync();
    var staticSeats = StaticSeats.Where(s => s.BusId == busId).ToList();

    var allSeats = staticSeats.Concat(dbSeats).ToList();

    if (!allSeats.Any())
    {
        allSeats = Enumerable.Range(1, 40)
            .Select(i => new Seat
            {
                Id = i,
                BusId = busId,
                SeatNo = $"S{i:D2}",
                IsBooked = false
            }).ToList();
    }

    return Ok(allSeats);
}
    }
}
