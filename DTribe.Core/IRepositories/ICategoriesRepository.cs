using DTribe.Core.DTO;
using DTribe.Core.Entities;
using DTribe.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTribe.Core.IRepositories
{
    public interface ICategoriesRepository
    {
        //Global categories
        Task<List<GlobalCategories>> GetGlobalCategoriesListAsync();

        //Global categories item list
        Task<List<GlobalCategoryItems>> GetGlobalCategoriesItemsListAsync(string categoryId);

        //User wise Catagory List
        Task<List<Categories>> GetUserCategoriesListAsync(string UserID);

        //User wise Catagory and Item List
        Task<List<CategoryItem>> GetUserCategoriesItemListAsync(string UserID,string categoryId);
        Task InsertCategory(string UserID, List<Categories> category);
        Task UpdateCategory(string UserID, List<Categories> category);
        Task DeleteCategory(string UserID, List<Categories> category);

        Task InsertCategorItem(string UserID, List<CategoryItem> category);
        Task UpdateCategorItem(string UserID, List<CategoryItem> category);
        Task DeleteCategorItem(string UserID, List<CategoryItem> category);
    }
}
