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
        Task<UserInfo> GetUserInfoAsync(string userID);
        Task Insert(UserInfo userinfo);
        Task Update(UserInfo userinfo);
        Task Delete(UserInfo userID);
    }
}
