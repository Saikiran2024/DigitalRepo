using DTribe.Core.Entities;
using DTribe.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTribe.Core.IRepositories
{
    public interface ICategoriesRepository
    {
        //Task<IEnumerable<UserCategories>> GetCategoriesSearchAsync(string searchString, string UserID, int distance, string distanceType, string sectionID, double userLatitude, double userLongitude, string city);

        Task<IEnumerable<UserCategories>> GetPostedList(string sectionID, string UserID, string distanceType, double userLatitude, double userLongitude, string city);
        Task<IEnumerable<UserCategoriesSearchResult>> GetCategoriesSearchBySPAsync(string searchString, string UserID, double userLatitude, double userLongitude, string distanceType, string city, string sectionID);
    }
}
