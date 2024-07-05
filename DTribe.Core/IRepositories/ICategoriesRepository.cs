using DTribe.Core.Entities;
using DTribe.DB.Entities;

namespace DTribe.Core.IRepositories
{
    public interface ICategoriesRepository
    {
        //Task<IEnumerable<UserCategories>> GetCategoriesSearchAsync(string searchString, string UserID, int distance, string distanceType, string sectionID, double userLatitude, double userLongitude, string city);

        Task<GlobalCategories> GetUserCategoriesAsync(string categoryID);
        Task<IEnumerable<UserCategoriesSearchResult>> GetPostedListBySearch(string searchString, string UserID, double userLatitude, double userLongitude, string distanceType, string city, string sectionID);

        Task<IEnumerable<UserCategoriesSearchResult>> GetPostedList(string UserID, double userLatitude, double userLongitude);
    }
}
