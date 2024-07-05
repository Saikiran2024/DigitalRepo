using AutoMapper;
using DTribe.Core.DTO;
using DTribe.Core.Entities;
using DTribe.Core.IRepositories;
using DTribe.Core.ResponseObjects;
using DTribe.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTribe.Core.Services.Auth
{

    public interface IAuthService
    {
        Task<StandardResponse<object>> NewSignupSendOTPToMobile(string mobilenumber);
        Task<StandardResponse<object>> CheckOTPAndCreateAccount(string mobileNumber, int otp);
        Task<StandardResponse<object>> SendOTPToMobileNumber(string mobileNumber);
        Task<StandardResponse<object>> CheckOTPGetLoginUserDetails(string mobileNumber, int? otp);
    }
    public class AuthService : IAuthService
    {
        private static IUserInfoRepository _userinfoRepository { get; set; }
        private readonly IMapper _mapper;
        public AuthService(IUserInfoRepository userinfoRepository, IMapper mapper)
        {
            _userinfoRepository = userinfoRepository;
            _mapper = mapper;
        }

        #region SignUp
        public async Task<StandardResponse<object>> NewSignupSendOTPToMobile(string mobilenumber)
        {
            var response = new StandardResponse<object>();

            //send otp to user mobile number here

            //and insert otp into TblUserTemp
            try
            {
                var usernewsignup = new UserTemp
                {
                    IDX = Guid.NewGuid(),
                    MobileNumber = mobilenumber,
                    OTP = 123,
                };

                await _userinfoRepository.InsertUserTempSignUp(usernewsignup);

                response.Status = ResponseStatus.Success;
                response.Data = usernewsignup.MobileNumber;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<StandardResponse<object>> CheckOTPAndCreateAccount(string mobileNumber, int otp)
        {
            var response = new StandardResponse<object>();
            UserTemp usertempinfo = await _userinfoRepository.GetUserTempOTP(mobileNumber);
            if (usertempinfo.MobileNumber != mobileNumber && usertempinfo.OTP != otp)
            {
                response.Status = ResponseStatus.Error;
                response.Message = $"OTP is Incorrect";
                response.Data = usertempinfo.MobileNumber;
            }
            string uscidSting = RandomStringGenerator.GenerateRandomString(20);
            //TODO: validations here
            var usernew = new UserInfo
            {
                IDX = Guid.NewGuid(),
                UserID = uscidSting,
                MobileNumber = mobileNumber,
                AccountStatus = "InActive",

                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };
            var usertempdelete = new UserTemp
            {
                MobileNumber = mobileNumber,
            };

            await _userinfoRepository.Insert(usernew);
            await _userinfoRepository.DeleteTempUser(usertempdelete);
            UserInfoDTO? userinfo = _mapper.Map<UserInfoDTO>(usernew);
            var token = await GenerateToken.GenerateJwtToken(userinfo);
            var responseData = new
            {
                token = token,
            };
            response.Status = ResponseStatus.Success;
            response.Data = responseData;
            return response;
        }

        #endregion

        #region Login
        public async Task<StandardResponse<object>> SendOTPToMobileNumber(string mobileNumber)
        {
            var response = new StandardResponse<object>();
            UserInfo? user = await _userinfoRepository.GetUserInfoByMobileNumberAsync(mobileNumber);
            if (user == null)
            {
                response.Status = ResponseStatus.Error;
                response.Message = "User not Available pls SignUp";
                response.Data = mobileNumber;
                return response;
            }
            //TODO: Send OTP to mobileNumber here

            //update otp in tbluser
            int otp = 123;
            await _userinfoRepository.UpdateOTP(mobileNumber, otp);
            //afetr send otp return success

            response.Status = ResponseStatus.Success;
            response.Data = mobileNumber;
            return response;
        }

        public async Task<StandardResponse<object>> CheckOTPGetLoginUserDetails(string mobileNumber, int? otp)
        {
            var response = new StandardResponse<object>();

            UserInfo? user = await _userinfoRepository.GetUserInfoByMobileNumberAsync(mobileNumber);

            if (otp == 123)
            {
                UserInfoDTO? userinfo = _mapper.Map<UserInfoDTO>(user);
                var token = await GenerateToken.GenerateJwtToken(userinfo);
                response.Data = token;
                response.Status = ResponseStatus.Success;
            }
            else
            {
                response.Status = ResponseStatus.Error;
                response.Message = "OTP is Incorrect";
            }

            return response;
        }

        #endregion
    }
}
