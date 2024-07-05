using AutoMapper;
using DTribe.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTribe.Core.DTO
{
    public class UserCategoriesSearchBySPDTO
    {
        public string? UserID { get; set; }
        [Key]
        public string? UserCategoryID { get; set; }
        public string? CategoryName { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? Rating { get; set; }
        public string? PostedTime { get; set; }
        public string? Distance { get; set; }
        public string? ImageID { get; set; }
    }
    public class PostedTimeResolver : IValueResolver<UserCategoriesSearchResult, UserCategoriesSearchBySPDTO, string>
    {
        public string Resolve(UserCategoriesSearchResult source, UserCategoriesSearchBySPDTO destination, string destMember, ResolutionContext context)
        {
            return CalculatePostedTime(source.CreatedDate);
        }

        private string CalculatePostedTime(DateTime? createdDate)
        {
            if (!createdDate.HasValue)
            {
                return "Date not available";
            }

            TimeSpan timeSpan = DateTime.Now - createdDate.Value;

            if (timeSpan.TotalHours >= 24)
            {
                return createdDate.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                int hours = (int)timeSpan.TotalHours;
                int minutes = timeSpan.Minutes;
                return $"{hours}h {minutes}m ago";
            }
        }
    }
}
