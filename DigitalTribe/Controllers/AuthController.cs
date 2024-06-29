using DigitalTribe.Helpers;
using DTribe.Core.DTO;
using DTribe.Core.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace DigitalTribe.Controllers
{
    [Route("api/Authentication")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        #region SignUp

        //Signup and loginProcess
        [Route("SignUp/{mobileNumber}"), HttpGet]
        public async Task<IActionResult> SignUp(string mobileNumber)
        {
            var response = await _authService.NewSignupSendOTPToMobile(mobileNumber);
            return ResponseHandler.Handle(response);
        }

        //cerate account when check the otp correct
        [Route("CreateAccount"), HttpPost]
        public async Task<IActionResult> CreateAccount(LoginModelDTO model)
        {
            var response = await _authService.CheckOTPAndCreateAccount(model.MobileNumber, model.OTP);
            return ResponseHandler.Handle(response);
        }

        #endregion

        #region login
        
        //send otp to mobilenumber for login
        [HttpGet("LoginWithMobile/{mobileNumber}")]
        public async Task<IActionResult> LoginSendOTP(string mobileNumber)
        {
            var response = await _authService.SendOTPToMobileNumber(mobileNumber);
            return ResponseHandler.Handle(response);
        }

        //login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModelDTO model)
        {
            var response = await _authService.CheckOTPGetLoginUserDetails(model.MobileNumber, model.OTP);
            return ResponseHandler.Handle(response);
        }

        #endregion

    }
}
