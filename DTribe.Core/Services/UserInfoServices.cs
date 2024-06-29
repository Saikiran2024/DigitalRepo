using AutoMapper;
using DTribe.Core.DTO;
using DTribe.Core.Entities;
using DTribe.Core.IRepositories;
using DTribe.Core.ResponseObjects;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DTribe.Core.Services
{
    public interface IUserInfoService
    {
        string GetUserId();
        Task<StandardResponse<UserInfoDTO>> GetUserInfo();
        Task<StandardResponse<object>> Update(UserInfoDTO userinfo);
        Task<StandardResponse<object>> Delete(string userID);
    }
    public class UserInfoServices : IUserInfoService
    {
        private static IUserInfoRepository _userinfoRepository { get; set; }
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserInfoServices(IUserInfoRepository userinfoRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userinfoRepository = userinfoRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            return userId.Value;
        }
        public async Task<StandardResponse<UserInfoDTO>> GetUserInfo()
        {
            var response = new StandardResponse<UserInfoDTO>();
            string userId=GetUserId();

            UserInfo? user = await _userinfoRepository.GetUserInfoAsync(userId);
            UserInfoDTO? userinfo = _mapper.Map<UserInfoDTO>(user);

            response.Status = ResponseStatus.Success;
            response.Data = userinfo;
            return response;
        }   
        public async Task<StandardResponse<object>> Update(UserInfoDTO userinfo)
        {
            var response = new StandardResponse<object>();
            try
            {
                //TODO: validations here
                var existingUser = await _userinfoRepository.GetUserInfoAsync(userinfo.UserID);

                if (existingUser == null)
                {
                    // Handle the case where the entity to be updated is not found
                    response.Status = ResponseStatus.Error;
                    response.AddError("User not found.");
                    return response;
                }
                //var usernew = new UserInfo
                //{
                existingUser.UserID = userinfo.UserID;
                existingUser.MobileNumber = userinfo.MobileNumber;
                existingUser.FullName = userinfo.FullName;
                existingUser.UserName = userinfo.UserName;
                existingUser.DOB = userinfo.DOB;
                existingUser.Age = userinfo.Age;
                existingUser.LocationID = userinfo.LocationID;
                existingUser.Gender = userinfo.Gender;
                existingUser.Latitude = userinfo.Latitude;
                existingUser.Longitude = userinfo.Longitude;
                existingUser.Language = userinfo.Language;
                existingUser.IsNotification = userinfo.IsNotification;
                existingUser.AccountStatus = "Active";

                existingUser.UpdatedDate = DateTime.Now;
                //};
                await _userinfoRepository.Update(existingUser);

                response.Status = ResponseStatus.Success;
                response.Message = $"" + userinfo.UserName + " User Created successful.";
                response.Data = existingUser.UserID;
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Fatal;
                response.AddError(FrequentErrors.InternalServerError);
                response.AddError(ex.Message);
                //response.AddError($"Error occurred while inserting data for Tenant: {tenantID}, User: {userID}");
                response.AddError(userinfo.ToString());

                //_logger.LogError(ex, "Error occurred while inserting data for Tenant: {TenantID}, User: {UserID}", tenantID, userID);
                throw;
            }

            return response;
        }
        public async Task<StandardResponse<object>> Delete(string userID)
        {
            var response = new StandardResponse<object>();

            try
            {
                // Retrieve the existing entity from the repository

                var existinguser = await _userinfoRepository.GetUserInfoAsync(userID);

                if (existinguser == null)
                {
                    // Handle the case where the entity to be deleted is not found
                    response.Status = ResponseStatus.Error;
                    response.AddError("Category not found.");
                    return response;
                }
                //UserCategories? categoriesdto = _mapper.Map<UserCategories>(category);
                // Remove the entity from the repository
                await _userinfoRepository.Delete(existinguser);

                response.Status = ResponseStatus.Success;
                response.Message = " User Delete successful.";
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Fatal;
                response.AddError(FrequentErrors.InternalServerError);
                response.AddError(ex.Message);

                // Add any additional error handling or logging as needed
                throw;
            }

            return response;
        }

        
    }

}
