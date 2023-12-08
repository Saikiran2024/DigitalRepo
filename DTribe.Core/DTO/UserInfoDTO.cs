using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTribe.Core.DTO
{
    public class UserInfoDTO
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string CategoryID { get; set; }
        public string Location { get; set; }
        public bool IsNotification { get; set; }
    }
}
