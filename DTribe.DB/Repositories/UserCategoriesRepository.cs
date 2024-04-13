using DTribe.Core.Entities;
using DTribe.Core.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DTribe.DB.Repositories
{

    public class UserCategoriesRepository : IUserCategoriesRepository
    {
        private readonly ApplicationDbContext _context;

        public UserCategoriesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserCategories>> CategoryWiseSearch(string UserID, string searchString, string SectionID)
        {
            IQueryable<UserCategories> categories;
            if (SectionID == null)
            {
                categories = _context.TblUserCategories.Where(n => (n.CategoryName.Contains(searchString) || n.Title.Contains(searchString)) || n.UserID == UserID).AsNoTracking();

            }
            else
            {
                categories = _context.TblUserCategories.Where(n => (n.CategoryName.Contains(searchString) || n.Title.Contains(searchString)) || n.SectionID == SectionID || n.UserID == UserID).AsNoTracking();


            }
            return categories;
        }
        public async Task<IEnumerable<UserCategories>> GetUserCategoriesListAsync(string UserID, string sectionID)
        {
            IQueryable<UserCategories>? category;
            if (sectionID == null)
            {
                category = _context.TblUserCategories.Where(n => n.UserID == UserID).AsNoTracking();
            }
            else
            {
                category = _context.TblUserCategories.Where(n => n.SectionID == sectionID || n.UserID == UserID).AsNoTracking();
            }
            return category;
        }
        public async Task<IEnumerable<UserCategories>> NearLocationWiseSearch(string UserID, string location)
        {
            throw new NotImplementedException();
        }
        public async Task<UserCategories> GetCategoryDetailsByIDX(string UserID, string Uscid)
        {
            UserCategories? category = await _context.TblUserCategories.Where(n => n.USCID == Uscid && n.UserID == UserID).SingleOrDefaultAsync();
            return category;
        }
        public async Task Insert(string UserID, UserCategories usercategory)
        {
            _context.TblUserCategories.Add(usercategory);
            await _context.SaveChangesAsync();
            //await _context.BulkInsertAsync(usercategory);
        }
        public async Task Update(string UserID, UserCategories usercategory)
        {
            _context.TblUserCategories.Update(usercategory);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(string UserID, UserCategories usercategory)
        {
            _context.TblUserCategories.Remove(usercategory);
            await _context.SaveChangesAsync();
        }











        //public async Task<List<GlobalSectionCategories>> GetGlobalCategoriesListAsync()
        //{
        //    List<GlobalCategories>? category = await _context.TblGlobalCategories.ToListAsync();
        //    return category;
        //}



        //public async Task<List<Categories>> GetUserCategoriesListAsync(string UserID)
        //{
        //    List<Categories>? category = await _context.TblCategories.Where(n => n.UserID == UserID).ToListAsync();
        //    return category;
        //}

        //public async Task<List<CategoryItem>> GetUserCategoriesItemListAsync(string UserID,string categoryId)
        //{
        //    List<CategoryItem>? category = await _context.TblCategoryItems.Where(n => n.UserID == UserID && n.CategoryID==categoryId).AsNoTracking().ToListAsync();
        //    return category;
        //}

        //public async Task InsertCategory(string UserID, List<Categories> category)
        //{
        //    await _context.BulkInsertAsync(category);
        //}

        //public async Task UpdateCategory(string UserID, List<Categories> category)
        //{
        //    foreach (var updatedCategory in category)
        //    {
        //        // Load the existing category from the database
        //        var existingCategory = await _context.TblCategories.FindAsync(updatedCategory.CategoryID);


        //        if (existingCategory != null)
        //        {
        //            // Update only the desired columns
        //            existingCategory.CategoryID = updatedCategory.CategoryID;
        //            existingCategory.CategoryName = updatedCategory.CategoryName;
        //        }
        //    }
        //    //await _context.BulkUpdateAsync(modifiedCategories);
        //    await _context.SaveChangesAsync();
        //    //await _context.BulkUpdateAsync(category, options => options.ColumnInputExpression = c => new { c.ColumnToIgnore });
        //}

        //public async Task DeleteCategory(string UserID, List<Categories> category)
        //{
        //    await _context.BulkDeleteAsync(category);
        //}

        //public async Task InsertCategorItem(string UserID, List<CategoryItem> category)
        //{
        //    await _context.BulkInsertAsync(category);
        //}

        //public async Task UpdateCategorItem(string UserID, List<CategoryItem> category)
        //{
        //    foreach (var updatedCategory in category)
        //    {
        //        // Load the existing category from the database
        //        var existingCategory = await _context.TblCategories.FindAsync(updatedCategory.CategoryID);


        //        if (existingCategory != null)
        //        {
        //            // Update only the desired columns
        //            existingCategory.CategoryID = updatedCategory.CategoryID;
        //            existingCategory.CategoryName = updatedCategory.CategoryName;
        //        }
        //    }
        //    //await _context.BulkUpdateAsync(modifiedCategories);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task DeleteCategorItem(string UserID, List<CategoryItem> category)
        //{
        //    foreach (var updatedCategory in category)
        //    {
        //        // Load the existing category from the database
        //        var existingCategory = await _context.TblCategoryItems.Where(n => n.UserID == UserID && n.CategoryItemID == updatedCategory.CategoryItemID).SingleOrDefaultAsync();

        //        if (existingCategory != null)
        //        {
        //            _context.TblCategoryItems.Remove(existingCategory);
        //        }
        //    }
        //    await _context.SaveChangesAsync();
        //}
    }
}
