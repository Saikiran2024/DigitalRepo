using DTribe.Core.DTO;
using DTribe.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace DigitalTribe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController(UserServices userservices)
        {
            userObj = userservices;
        }
        //UserDA userObj = new UserDA();
        //private readonly UserDA userObj;
        JObject _response = new JObject();
        private UserServices userObj;

        [Route("Profile/{userID}"), HttpGet]
        public IActionResult UserDetails(string userID)
        {
            CategoriesDTO UserDetails;

            try
            {
                //UserDetails = userObj.userdetails(userID);
            }
            catch (Exception) { return BadRequest(); }

            //return Content(UserResponse.OK().HttpRespCode, ReturnResponse(UserResponse.OK(), UserDetails, NoOfRecords.single.ToString()));
            return Ok(new
            {
                Status = "OK",
                //Data = UserDetails,
                Message = "User details retrieved successfully"
            }); ;

        }
    }
}
