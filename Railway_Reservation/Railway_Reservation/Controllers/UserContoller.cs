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
    public class UserController : ControllerBase
    {
        private IUserInterface context;
        private IEmailInterface context1;
        public UserController(IUserInterface context, IEmailInterface context1)
        {
            this.context = context;
            this.context1 = context1;
        }

        [HttpPost("Signup")]
        [AllowAnonymous]
        public ActionResult SignUp(User user)
        {
            var user1 = context.Authenticate(user.PhoneNo, user.Password);
            if (user1 != null)
            {
                return BadRequest("User already exist.");
            }
            else
            {
                context.Add(user);
                string Subject = $"Welcome {user.Name} to Railway Reservation System";
                string Body = "You are successfully signed up.";
                context1.SendEmail(user.Email, Subject, Body);
                return Ok("User registered successfully.");
            }
        }

        [HttpGet("UserDetails")]
        public IActionResult UserDetails()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim.Value);
            var User1 = context.GetUser(userId);
            return Ok($"Name = {User1.Name}\nEmail = {User1.Email}\nPhone Number = {User1.PhoneNo}");
        }

        [HttpPut("Update_User_Details")]
        public ActionResult UpdateFlight(User user)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            int id = int.Parse(userIdClaim.Value);
            context.Update(id, user);
            return Ok("Flight updated successfully");
        }

        [HttpDelete("Delete_User")]
        public IActionResult DeleteUser()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim.Value);
            context.Delete(userId);
            return Ok("The User has been deleted");
        }
    }
}
