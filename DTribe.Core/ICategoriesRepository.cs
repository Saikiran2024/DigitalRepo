using DTribe.Core.DTO;
using DTribe.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTribe.Core
{
    public interface ICategoriesRepository
    {
        Task<Categories> GetCategoriesAsync(string categoryID);

        Task Insert(string UserID, Categories category);
        Task Update(string UserID, Categories category);
    }
}
