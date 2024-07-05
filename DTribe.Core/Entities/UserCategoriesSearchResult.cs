using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTribe.Core.Entities
{
    public class UserCategoriesSearchResult
    {
        public string? UserID { get; set; }
        [Key]
        public string? UserCategoryID { get; set; }
        public string? CategoryName { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? Rating { get; set; }
        public DateTime? CreatedDate { get; set; }
        public double? Distance { get; set; }
        public string? ImageID { get; set; }
    }
}
