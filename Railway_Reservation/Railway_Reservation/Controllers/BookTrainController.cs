using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Railway_Reservation.Model;
using Railway_Reservation.Repository;
using System.Security.Claims;

namespace Railway_Reservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class BookTrainController : ControllerBase
    {
        private IBookInterface context;
        private ITraininterface context1;
        private IEmailInterface context2;
        private IUserInterface context3;
        private ILogger<BookTrainController> logger;
        public BookTrainController(IBookInterface context, ITraininterface context1, IEmailInterface context2, IUserInterface context3, ILogger<BookTrainController> logger)
        {
            this.context = context;
            this.context1 = context1;
            this.context2 = context2;
            this.context3 = context3;
            this.logger = logger;
        }

        [HttpPost("Search_Train")]
        [AllowAnonymous]
        public IActionResult Search_Train(TrainSearch train)
        {
            var train_ = context1.Search(train.ArrivalCity, train.DepartureCity, train.NoOfSeats, train.Class);
            logger.LogInformation("Searching available Flights");
            return Ok(train_);
        }

        [HttpPost("Book_Train")]
        public IActionResult Book_Train(int id, int noOfSeats)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim.Value);
            int result = context.Book(id, noOfSeats, userId);
            if (result == 2)
            {
                logger.LogInformation("Train booked by user");
                return Ok("Train Booked Successfully");
            }
            else if (result == 1)
            {
                logger.LogError("No such train in database");
                return BadRequest("No such train is available");
            }
            else
            {
                logger.LogError("Seats are not available");
                return BadRequest("Seats not avilable");
            }
        }

        [HttpGet("AllBookings")]
        public IActionResult GetAllBookings()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim.Value);
            logger.LogInformation("Getting list of all the bookings by user");
            return Ok(context.GetTrainList(userId));
        }

        [HttpPost("Checkout")]
        public IActionResult CheckoutBooking(int id, int Payment)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim.Value);
            var user = context3.GetUser(userId);
            int result = context.Checkout(id, Payment);
            if (result == 1)
            {
                logger.LogError("Booking not found for the id entered by user");
                return BadRequest("No such booking found");
            }
            else if (result == 2)
            {
                logger.LogInformation("Transaction successfull for the booking");
                string Subject = "Train Booked";
                string Body = "The Train is successfully booked";
                context2.SendEmail(user.Email, Subject, Body);
                return Ok("Congratulation! The Payment Is Successfull.\nEnjoy Your Journey.");
            }
            else if (result == 3)
            {
                logger.LogError("Transaction failed");
                return BadRequest("Payment failed\nNo amount was deducted");
            }
            else
            {
                logger.LogError("Seats are not available");
                return BadRequest("Sorry seats are not available");
            }
        }

        [HttpPost("Cancel_Train")]
        public IActionResult Cancel_Train(int id)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim.Value);
            bool result = context.Cancel(id);
            if (result)
            {
                string Subject = "Train Cancelled";
                string Body = "The train is successfully cancelled";
                var user = context3.GetUser(userId);
                context2.SendEmail(user.Email, Subject, Body);
                logger.LogInformation("Booking cancelled");
                return Ok("Booking has been cancelled\nThe amount has been refunded");
            }
            else
            {
                logger.LogError("Unable to cancel the train");
                return BadRequest("No Such Booking Found");
            }
        }
    }
}
