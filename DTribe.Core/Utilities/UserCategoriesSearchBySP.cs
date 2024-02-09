using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTribe.Core.Utilities
{
    public class UserCategoriesSearchBySP
    {
        public string UserID { get; set; }
        public string USCID { get; set; }
        public string CategoryName { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int? Rating { get; set; }
        public string? PostedTime { get; set; }
        public string Distance { get; set; }

    }
}
