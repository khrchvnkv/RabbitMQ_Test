using FormulaAirline.API.Models;
using FormulaAirline.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormulaAirline.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IMessageProducer _messageProducer;
        private readonly ILogger<BookingsController> _logger;
        
        // In-memory db
        public static readonly List<Booking> Bookings = new();

        public BookingsController(IMessageProducer messageProducer, ILogger<BookingsController> logger)
        {
            _messageProducer = messageProducer;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult CreateBooking(Booking newBooking)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            Bookings.Add(newBooking);
            _messageProducer.SendMessage(newBooking);
            return Ok();
        }
    }
}