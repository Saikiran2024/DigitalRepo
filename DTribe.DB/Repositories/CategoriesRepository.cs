using DTribe.Core;
using DTribe.DB.Entities;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTribe.DB.Repositories
{

    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoriesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Categories> GetCategoriesAsync(string categoryID)
        {
            Categories? category= await _context.TblCategories.Where(n=>n.CategoryID==categoryID).SingleOrDefaultAsync();
            return category;         
        }

        public async Task Insert(string UserID, Categories category)
        {
             _context.TblCategories.Add(category);
             _context.SaveChanges();
        }

        public async Task Update(string UserID, Categories category)
        {
            _context.TblCategories.Update(category);
            _context.SaveChanges();
        }
    }
}
