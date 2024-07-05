using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTribe.DB.Entities
{
    [Table("TblGlobalCategories")]
    public class GlobalCategories
    {
        public Guid IDX { get; set; }   
        public string SectionID { get; set; }
        [Key]
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
