using DigitalTribe.Helpers;
using DTribe.Core.DTO;
using DTribe.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigitalTribe.Controllers
{
    [Authorize]
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserInfoService _UserinfoService;
        public UserController(IUserInfoService userinfoService)
        {
            _UserinfoService = userinfoService;
        }     

        [HttpGet("Profile")]
        public async Task<IActionResult> UserDetails()
        {
            //foreach (var claim in User.Claims)
            //{
            //    Console.WriteLine($"{claim.Type}: {claim.Value}");
            //}
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            var response = await _UserinfoService.GetUserInfo(userId);
            return ResponseHandler.Handle(response);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateUserDetails(UserInfoDTO userinfo)
        {
            var response = await _UserinfoService.Update(userinfo);
            return ResponseHandler.Handle(response);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string userID)
        {
            var response = await _UserinfoService.Delete(userID);
            return ResponseHandler.Handle(response);
        }

        
    }
}
