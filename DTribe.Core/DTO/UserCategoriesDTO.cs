﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTribe.Core.DTO
{
    public class UserCategoriesDTO
    {
        public string? UserCategoryID { get; set; }
        public string SectionID { get; set; }
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string? ImageID { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int DistanceLocation { get; set; }
        public int? Rating { get; set; }
        public decimal? Price { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? CityLocationID { get; set; }
        public bool? IsSystemImage { get; set; }
    }

    public class UserCategoriesAddDTO
    {
        public string CategoryID { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }

    }

    public class UserCategoriesUpdateDTO
    {
        public string UserCategoryID { get; set; }
        public string CategoryID { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
    }
}
