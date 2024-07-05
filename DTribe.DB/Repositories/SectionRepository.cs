using DTribe.Core.Entities;
using DTribe.Core.IRepositories;
using DTribe.DB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTribe.DB.Repositories
{
    public class SectionRepository : ISectionRepository
    {
        private readonly ApplicationDbContext _context;

        public SectionRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<GlobalCategories>> GetSectionCategoryList(string SectionID)
        {
            List<GlobalCategories>? sections = await _context.TblGlobalCategories.Where(n=>n.SectionID==SectionID).ToListAsync();
            return sections;
        }

        public async Task<List<Section>> GetSectionList()
        {
            List<Section>? sections = await _context.TblSection.ToListAsync();
            return sections;

        }
    }
}
