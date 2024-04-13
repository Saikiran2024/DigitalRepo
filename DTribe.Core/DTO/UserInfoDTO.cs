using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTribe.Core.DTO
{
    public class UserInfoDTO
    {
        public string UserID { get; set; }
        public string? FullName { get; set; }
        public string UserName { get; set; }
        public string MobileNumber { get; set; }
        public int? Age { get; set; }
        public DateTime? DOB { get; set; }
        public string? Gender { get; set; }
        public string? Language { get; set; }
        public string? LocationID { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        //public SqlGeography Coordinates { get; set; }
        public bool? IsNotification { get; set; }
    }
}
