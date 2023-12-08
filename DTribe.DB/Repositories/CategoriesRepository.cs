using DTribe.Core.Entities;
using DTribe.Core.IRepositories;
using DTribe.DB.Entities;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace DTribe.DB.Repositories
{

    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoriesRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<GlobalCategories>> GetGlobalCategoriesListAsync()
        {
            List<GlobalCategories>? category = await _context.TblGlobalCategories.ToListAsync();
            return category;
        }

        public async Task<List<GlobalCategoryItems>> GetGlobalCategoriesItemsListAsync(string categoryId)
        {
            List<GlobalCategoryItems>? category = await _context.TblGlobalCategoryItems.Where(n=>n.CategoryID==categoryId).AsNoTracking().ToListAsync();
            return category;
        }

        public async Task<List<Categories>> GetUserCategoriesListAsync(string UserID)
        {
            List<Categories>? category = await _context.TblCategories.Where(n => n.UserID == UserID).ToListAsync();
            return category;
        }

        public async Task<List<CategoryItem>> GetUserCategoriesItemListAsync(string UserID,string categoryId)
        {
            List<CategoryItem>? category = await _context.TblCategoryItems.Where(n => n.UserID == UserID && n.CategoryID==categoryId).AsNoTracking().ToListAsync();
            return category;
        }

        public async Task InsertCategory(string UserID, List<Categories> category)
        {
            await _context.BulkInsertAsync(category);
        }

        public async Task UpdateCategory(string UserID, List<Categories> category)
        {
            foreach (var updatedCategory in category)
            {
                // Load the existing category from the database
                var existingCategory = await _context.TblCategories.FindAsync(updatedCategory.CategoryID);


                if (existingCategory != null)
                {
                    // Update only the desired columns
                    existingCategory.CategoryID = updatedCategory.CategoryID;
                    existingCategory.CategoryName = updatedCategory.CategoryName;
                }
            }
            //await _context.BulkUpdateAsync(modifiedCategories);
            await _context.SaveChangesAsync();
            //await _context.BulkUpdateAsync(category, options => options.ColumnInputExpression = c => new { c.ColumnToIgnore });
        }

        public async Task DeleteCategory(string UserID, List<Categories> category)
        {
            await _context.BulkDeleteAsync(category);
        }

        public async Task InsertCategorItem(string UserID, List<CategoryItem> category)
        {
            await _context.BulkInsertAsync(category);
        }

        public async Task UpdateCategorItem(string UserID, List<CategoryItem> category)
        {
            foreach (var updatedCategory in category)
            {
                // Load the existing category from the database
                var existingCategory = await _context.TblCategories.FindAsync(updatedCategory.CategoryID);


                if (existingCategory != null)
                {
                    // Update only the desired columns
                    existingCategory.CategoryID = updatedCategory.CategoryID;
                    existingCategory.CategoryName = updatedCategory.CategoryName;
                }
            }
            //await _context.BulkUpdateAsync(modifiedCategories);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategorItem(string UserID, List<CategoryItem> category)
        {
            foreach (var updatedCategory in category)
            {
                // Load the existing category from the database
                var existingCategory = await _context.TblCategoryItems.Where(n => n.UserID == UserID && n.CategoryItemID == updatedCategory.CategoryItemID).SingleOrDefaultAsync();

                if (existingCategory != null)
                {
                    _context.TblCategoryItems.Remove(existingCategory);
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
