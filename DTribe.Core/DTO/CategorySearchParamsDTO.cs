using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTribe.Core.DTO
{
    public class CategorySearchParamsDTO
    {
        public string? searchString { get; set; }
        public int distance { get; set; }
        public string? distanceType { get; set; }
        public string? sectionID { get; set; }
        public double userLatitude { get; set; }
        public double userLongitude { get; set; }
        public string? city { get; set; }
    }
}
