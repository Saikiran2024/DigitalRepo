﻿using AutoMapper;
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
using static System.Net.WebRequestMethods;

namespace DTribe.Core.Services
{
    public interface IUserInfoService
    {
        Task<StandardResponse<UserInfoDTO>> GetUserInfo(string UserID);
        Task<StandardResponse<object>> CheckOTP(string mobileNumber, string otp);
        Task<StandardResponse<object>> SendOTPToMobileNumber(string mobileNumber);
        Task<StandardResponse<object>> Insert(UserInfoDTO userinfo);
        Task<StandardResponse<object>> Update(UserInfoDTO userinfo);
        Task<StandardResponse<object>> Delete(string userID);
    }
    public class UserInfoServices : IUserInfoService
    {

        private static IUserInfoRepository _userinfoRepository { get; set; }
        private readonly IMapper _mapper;
        public UserInfoServices(IUserInfoRepository userinfoRepository, IMapper mapper)
        {
            _userinfoRepository = userinfoRepository;
            _mapper = mapper;
        }
        public async Task<StandardResponse<UserInfoDTO>> GetUserInfo(string UserID)
        {
            var response = new StandardResponse<UserInfoDTO>();

            UserInfo? user = await _userinfoRepository.GetUserInfoAsync(UserID);
            UserInfoDTO? userinfo = _mapper.Map<UserInfoDTO>(user);

            response.Status = ResponseStatus.Success;
            response.Data = userinfo;
            return response;
        }

        public async Task<StandardResponse<object>> Insert(UserInfoDTO userinfo)
        {
            var response = new StandardResponse<object>();
            try
            {
                string uscidSting = RandomStringGenerator.GenerateRandomString(20);
                //TODO: validations here
                var usernew = new UserInfo
                {
                    IDX = Guid.NewGuid(),
                    UserID = uscidSting,
                    MobileNumber = userinfo.MobileNumber,
                    FullName = userinfo.FullName,
                    UserName = userinfo.UserName,
                    DOB = userinfo.DOB,
                    Gender = userinfo.Gender,
                    Latitude = userinfo.Latitude,
                    Longitude = userinfo.Longitude,
                    Language = userinfo.Language,

                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                };
                await _userinfoRepository.Insert(usernew);

                response.Status = ResponseStatus.Success;
                response.Message = $"" + userinfo.UserName + " User Created successful.";
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

        public Task<StandardResponse<object>> Update(UserInfoDTO userinfo)
        {
            throw new NotImplementedException();
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

        public async Task<StandardResponse<object>> CheckOTP(string mobileNumber, string otp)
        {
            var response = new StandardResponse<object>();

            if (mobileNumber == "987654321" && otp == "123")
            {
                response.Status = ResponseStatus.Success;
            }
            else
            {
                response.Status = ResponseStatus.Error;
            }

            return response;
        }

        public async Task<StandardResponse<object>> SendOTPToMobileNumber(string mobileNumber)
        {
            var response = new StandardResponse<object>();
            //afetr send otp return success
            response.Status = ResponseStatus.Success;

            return response;
        }
    }
}