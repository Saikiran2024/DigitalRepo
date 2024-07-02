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
            var response = await _UserinfoService.GetUserInfo();
            return ResponseHandler.Handle(response);
        }

        [HttpGet("AddImage")]
        public async Task<IActionResult> AddImage()
        {
            var response = await _UserinfoService.AddUserImage();
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
