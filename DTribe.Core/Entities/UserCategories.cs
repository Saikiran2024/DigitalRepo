﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTribe.Core.Entities
{
    [Table("TblUserCategories")]
    public class UserCategories
    {
        
        public Guid IDX { get; set; }
        [Key]
        public string UserCategoryID { get; set; }
        public string UserID { get; set; }
        public string SectionID { get; set; }
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string? ImageID { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        //public int? DistanceLocation { get; set; }
        public int? Rating { get; set; }
        public decimal? Price { get; set; }
        public double Latitude {  get; set; }
        public double Longitude { get; set; }
        public string CityLocationID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }     
        public string ImageUrl { get; set; }
        public bool IsSystemImage { get; set; }

    }
}
