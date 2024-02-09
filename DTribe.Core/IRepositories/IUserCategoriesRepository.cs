using DTribe.Core.DTO;
using DTribe.Core.Entities;
using DTribe.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace DTribe.Core.IRepositories
{
    public interface IUserCategoriesRepository
    {
        //Global categories wise details
        Task<UserCategories> GetCategoryDetailsByIDX(string UserID, string Uscid);
        Task<IEnumerable<UserCategories>> GetUserCategoriesListAsync(string UserID, string sectionID);
        Task<IEnumerable<UserCategories>> CategoryWiseSearch(string UserID, string searchString,string sectionID);
        Task<IEnumerable<UserCategories>> NearLocationWiseSearch(string UserID, string location);
        Task Insert(string UserID, UserCategories usercategory);
        Task Update(string UserID, UserCategories usercategory);
        Task Delete(string UserID, UserCategories usercategory);







    }
}
