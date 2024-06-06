using DigitalTribe.Helpers;
using DTribe.Core.DTO;
using DTribe.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace DigitalTribe.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserInfoService _UserinfoService;
        public UserController(IUserInfoService userinfoService)
        {
            _UserinfoService = userinfoService;
        }

        [Route("Profile/{userID}"), HttpGet]
        public async Task<IActionResult> UserDetails(string userID)
        {
            var response = await _UserinfoService.GetUserInfo(userID);
            return ResponseHandler.Handle(response);
        }

        [Route("CheckOTP/{mobileNumber}/{otp}"), HttpGet]
        public async Task<IActionResult> CheckOTP(string mobileNumber,string otp)
        {
            var response = await _UserinfoService.CheckOTPAndCreateAccount(mobileNumber,otp);
            return ResponseHandler.Handle(response);
        }

        [Route("SendOTP/{mobileNumber}/{otp}"), HttpGet]
        public async Task<IActionResult> SendOTP(string mobileNumber)
        {
            var response = await _UserinfoService.SendOTPToMobileNumber(mobileNumber);
            return ResponseHandler.Handle(response);
        }

        [HttpPost("LoginCheckMobileNumber/{mobileNumber}")]
        public async Task<IActionResult> Login(string mobileNumber)
        {
            var response = await _UserinfoService.SendOTPToMobileNumber(mobileNumber);
            return ResponseHandler.Handle(response);
        }

        [HttpPost("LoginUser/{mobileNumber}/{otp}")]
        public async Task<IActionResult> LoginUser(string mobileNumber,string otp)
        {
            var response = await _UserinfoService.LoginUser(mobileNumber,otp);
            return ResponseHandler.Handle(response);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateUserDetails(UserInfoDTO userinfo)
        {
            var response = await _UserinfoService.Update(userinfo);
            return ResponseHandler.Handle(response);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteDetails(string userID)
        {
            var response = await _UserinfoService.Delete(userID);
            return ResponseHandler.Handle(response);
        }
    }
}
