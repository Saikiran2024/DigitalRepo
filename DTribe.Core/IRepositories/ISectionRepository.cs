using DTribe.Core.Entities;
using DTribe.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTribe.Core.IRepositories
{
    public interface ISectionRepository
    {
        Task<List<Section>> GetSectionList();

        Task<List<GlobalCategories>> GetSectionCategoryList(string SectionID);
    }
}
