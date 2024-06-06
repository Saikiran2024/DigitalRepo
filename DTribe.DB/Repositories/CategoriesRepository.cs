using DTribe.Core.Entities;
using DTribe.Core.IRepositories;
using DTribe.Core.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace DTribe.DB.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoriesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserCategoriesSearchResult>> GetCategoriesSearchBySPAsync(string searchString, string UserID, double userLatitude, double userLongitude, string distanceType, string city, string sectionID)
        {
            IEnumerable<UserCategoriesSearchResult> result = _context.SPGetCategoriesNearByLocation(searchString, UserID, userLatitude, userLongitude, distanceType, city, sectionID);
            return result;
        }


        public async Task<IEnumerable<UserCategories>> GetCategoriesSearchAsync(string searchString, string UserID, int distance, string distanceType, string sectionID, double userLatitude, double userLongitude, string city)
        {
            //Location referenceLocation = new Location { Latitude = 37.7749, Longitude = -122.4194 };

            IQueryable<UserCategories> categories;

            if (sectionID == null)
            {
                switch (distanceType)
                {
                    case "Nearby":
                        categories = _context.TblUserCategories
                        .Where(userLocation => GeoCalculator.CalculateHaversineDistance(
                                new Location { Latitude = userLocation.Latitude, Longitude = userLocation.Longitude },
                                new Location { Latitude = userLatitude, Longitude = userLongitude }) > 10.0 && userLocation.UserID != UserID && (userLocation.CategoryName.Contains(searchString) || userLocation.Title.Contains(searchString)))
                        .AsNoTracking();
                        break;

                    case "Nationwide":
                        categories = _context.TblUserCategories.Where(n => n.UserID != UserID && n.Rating.HasValue && (n.CategoryName.Contains(searchString) || n.Title.Contains(searchString))).OrderByDescending(n => n.Rating).AsNoTracking();
                        break;

                    case "Explore":
                        categories = _context.TblUserCategories.Where(n => n.UserID != UserID && n.CityLocationID == city && (n.CategoryName.Contains(searchString) || n.Title.Contains(searchString))).AsNoTracking();
                        break;

                    // Add more cases as needed

                    default:
                        categories = _context.TblUserCategories.Where(n => n.UserID != UserID).AsNoTracking();
                        break;
                }

            }
            else
            {
                switch (distanceType)
                {
                    case "Nearby":
                        categories = _context.TblUserCategories
                        .Where(userLocation => GeoCalculator.CalculateHaversineDistance(
                                new Location { Latitude = userLocation.Latitude, Longitude = userLocation.Longitude },
                                new Location { Latitude = userLatitude, Longitude = userLongitude }) > 10.0 && userLocation.UserID != UserID && userLocation.SectionID == sectionID && (userLocation.CategoryName.Contains(searchString) || userLocation.Title.Contains(searchString)))
                        .AsNoTracking();

                        break;

                    case "Nationwide":
                        categories = _context.TblUserCategories.Where(n => n.UserID != UserID && n.SectionID == sectionID && n.Rating.HasValue && (n.CategoryName.Contains(searchString) || n.Title.Contains(searchString))).OrderByDescending(n => n.Rating).AsNoTracking();
                        break;

                    case "Explore":
                        categories = _context.TblUserCategories.Where(n => n.UserID != UserID || n.SectionID == sectionID && n.CityLocationID == city && (n.CategoryName.Contains(searchString) || n.Title.Contains(searchString))).AsNoTracking();
                        break;

                    // Add more cases as needed

                    default:
                        categories = _context.TblUserCategories.Where(n => n.UserID != UserID || n.SectionID == sectionID).AsNoTracking();
                        break;
                }


            }
            return await categories.ToListAsync();
        }

        public async Task<IEnumerable<UserCategories>> GetPostedList(string sectionID, string UserID,  string distanceType, double userLatitude, double userLongitude, string city)
        {
            //Location referenceLocation = new Location { Latitude = 37.7749, Longitude = -122.4194 };

            IQueryable<UserCategories> categories;

            if (sectionID == null)
            {
                switch (distanceType)
                {
                    case "Nearby":
                        //categories = _context.TblUserCategories
                        //.Where(userLocation => GeoCalculator.CalculateHaversineDistance(
                        //        new Location { Latitude = userLocation.Latitude, Longitude = userLocation.Longitude },
                        //        new Location { Latitude = userLatitude, Longitude = userLongitude }) > 10.0 && userLocation.UserID != UserID )
                        //.AsNoTracking();
                        categories = _context.TblUserCategories.Where(n => n.UserID != UserID && n.Rating.HasValue).OrderByDescending(n => n.Rating).AsNoTracking();
                        break;

                    case "Nationwide":
                        categories = _context.TblUserCategories.Where(n => n.UserID != UserID && n.Rating.HasValue ).OrderByDescending(n => n.Rating).AsNoTracking();
                        break;

                    case "Explore":
                        categories = _context.TblUserCategories.Where(n => n.UserID != UserID && n.CityLocationID == city).AsNoTracking();
                        break;

                    // Add more cases as needed

                    default:
                        categories = _context.TblUserCategories.Where(n => n.UserID != UserID).AsNoTracking();
                        break;
                }

            }
            else
            {
                switch (distanceType)
                {
                    case "Nearby":
                        categories = _context.TblUserCategories
                        .Where(userLocation => GeoCalculator.CalculateHaversineDistance(
                                new Location { Latitude = userLocation.Latitude, Longitude = userLocation.Longitude },
                                new Location { Latitude = userLatitude, Longitude = userLongitude }) > 10.0 && userLocation.UserID != UserID && userLocation.SectionID == sectionID )
                        .AsNoTracking();

                        break;

                    case "Nationwide":
                        categories = _context.TblUserCategories.Where(n => n.UserID != UserID && n.SectionID == sectionID && n.Rating.HasValue ).OrderByDescending(n => n.Rating).AsNoTracking();
                        break;

                    case "Explore":
                        categories = _context.TblUserCategories.Where(n => n.UserID != UserID || n.SectionID == sectionID && n.CityLocationID == city ).AsNoTracking();
                        break;

                    // Add more cases as needed

                    default:
                        categories = _context.TblUserCategories.Where(n => n.UserID != UserID || n.SectionID == sectionID).AsNoTracking();
                        break;
                }


            }
            return await categories.ToListAsync();
        }


    }
}
