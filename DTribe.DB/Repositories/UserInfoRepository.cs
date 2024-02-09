using DTribe.Core.Entities;
using DTribe.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace DTribe.DB.Repositories
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly ApplicationDbContext _context;

        public UserInfoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<UserInfo> GetUserInfoAsync(string userID)
        {
            UserInfo result = await _context.TblUser.FindAsync(userID);
            return result;
        }

        public async Task Insert(UserInfo userinfo)
        {
            _context.TblUser.Add(userinfo);
            await _context.SaveChangesAsync();
        }

        public async Task Update(UserInfo userinfo)
        {
            _context.TblUser.Update(userinfo);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(UserInfo userinfo)
        {
            // Remove the entity from the context
            _context.TblUser.Remove(userinfo);
            // Save changes to the database
            await _context.SaveChangesAsync();
        }
    }
}
