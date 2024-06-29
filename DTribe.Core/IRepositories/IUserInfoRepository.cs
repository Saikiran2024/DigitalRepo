using DTribe.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTribe.Core.IRepositories
{
    public interface IUserInfoRepository
    {
        Task<UserInfo> GetUserInfoByMobileNumberAsync(string mobileNUmber);
        Task<UserInfo> GetUserInfoAsync(string userID);
        Task<UserTemp> GetUserTempOTP(string mobilenumber);
        Task InsertUserTempSignUp(UserTemp userinfo);
        Task Insert(UserInfo userinfo);
        Task Update(UserInfo userinfo);
        Task Delete(UserInfo userID);
        Task DeleteTempUser(UserTemp usertemp);
        Task UpdateOTP(string mobilenumber, int newOTP);
    }
}
