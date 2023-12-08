using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTribe.Core.DTO
{
    public class CategoryItemDTO
    {
        public string UserID { get; set; }
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        [Key]
        public string CategoryItemID { get; set; }
        public string CategoryItemName { get; set; }
    }
}
