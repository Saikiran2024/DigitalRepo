using DTribe.Core.Entities;
using DTribe.Core.IRepositories;
using Microsoft.EntityFrameworkCore;

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
        public async Task<UserInfo> GetUserInfoByMobileNumberAsync(string mobileNUmber)
        {
            UserInfo? result = await _context.TblUser.Where(n => n.MobileNumber == mobileNUmber).SingleOrDefaultAsync();
            return result;
        }
        public async Task<UserTemp> GetUserTempOTP(string mobilenumber)
        {
            UserTemp result = await _context.TblUserTemp.FindAsync(mobilenumber);
            return result;
        }
        public async Task InsertUserTempSignUp(UserTemp userinfo)
        {
            _context.TblUserTemp.Add(userinfo);
            await _context.SaveChangesAsync();
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

        public async Task UserImageUpdate(UserInfo userinfo)
        {
            // Attach the user entity to the context
            _context.TblUser.Attach(userinfo);

            // Mark the Image property as modified
            _context.Entry(userinfo).Property(u => u.UserImageID).IsModified = true;

            // Save changes to the database
            await _context.SaveChangesAsync();

        }
        public async Task UpdateOTP(string mobilenumber, int newOTP)
        {
            var user = await _context.TblUser.FirstOrDefaultAsync(u => u.MobileNumber == mobilenumber);

            // Check if the user exists
            if (user != null)
            {
                // Update the OTP field
                user.OTP = newOTP;

                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            else
            {
                // Handle the case where the user was not found, if needed
                // For example, you might log a message or throw an exception
            }
        }
        public async Task Delete(UserInfo userinfo)
        {
            // Remove the entity from the context
            _context.TblUser.Remove(userinfo);
            // Save changes to the database
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTempUser(UserTemp usertemp)
        {
            // Remove the entity from the context
            _context.TblUserTemp.Remove(usertemp);
            // Save changes to the database
            await _context.SaveChangesAsync();
        }
       
    }
}
