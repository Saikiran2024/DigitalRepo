using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTribe.Core.Entities
{
    public class UserInfo
    {
        public Guid IDX { get; set; }
        [Key]
        public string UserID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string CategoryID { get; set; }
        public int LocationID { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        //public SqlGeography Coordinates { get; set; }
        public bool IsNotification { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
