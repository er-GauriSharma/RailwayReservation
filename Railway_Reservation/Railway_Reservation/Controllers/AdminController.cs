using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Railway_Reservation.Model;
using Railway_Reservation.Data;
using Railway_Reservation.Repository;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Security.Claims;

namespace Railway_Reservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private IUserInterface context;
        private ITraininterface context2;
        private ILogger<AdminController> logger;
        public AdminController(IUserInterface context, IAdminInterface context1, ITraininterface context2, ILogger<AdminController> logger)
        {
            this.context = context;
            this.context2 = context2;
            this.logger = logger;
        }

        [HttpPost("AddTrain")]
        public ActionResult AddTrain(Train train)
        {
            context2.Add(train);
            logger.LogInformation("Train Added");
            return Ok("Train registered successfully");
        }

        [HttpGet("ListOfTrains")]
        public IActionResult ListOfTrains()
        {
            logger.LogInformation("Getting List of all the trains available");
            return Ok(context2.GetTrainsList());
        }

        [HttpPut("Update_Trains")]
        public ActionResult UpdateTrain(int id, Train train)
        {
            context2.Update(id, train);
            logger.LogInformation("Train Updated");
            return Ok("Train updated successfully");
        }

        [HttpDelete("Delete_Train")]
        public IActionResult DeleteTrain(int id)
        {
            context2.Delete(id);
            logger.LogInformation("Train Deleted");
            return Ok("The Train has been deleted");
        }

        [HttpGet("ListOfUser")]
        public IActionResult ListOfUsers()
        {
            logger.LogInformation("Getting List of all the users registered");
            return Ok(context.GetUserList());
        }
    }
}
