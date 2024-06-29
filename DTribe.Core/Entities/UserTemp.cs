using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTribe.Core.Entities
{
    public class UserTemp
    {
        public Guid IDX { get; set; }
        [Key]
        public string MobileNumber { get; set; }
        public int? OTP { get; set; }
    }
}
