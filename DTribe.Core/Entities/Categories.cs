using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTribe.DB.Entities
{
    public class Categories
    {
        public Guid IDX { get; set; }
        [Key]
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
