using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTribe.Core.Entities
{
    public class CategoryItem
    {
        public Guid? IDX { get; set; }
        public string UserID { get; set; }
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        [Key]
        public string CategoryItemID { get; set; }
        public string CategoryItemName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
