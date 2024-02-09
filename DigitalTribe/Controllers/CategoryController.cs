using DigitalTribe.Helpers;
using DTribe.Core.DTO;
using DTribe.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalTribe.Controllers
{
    [Route("api/Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public readonly ICategoriesService _categoryService;
        public CategoryController(ICategoriesService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("Search")]  //if sectionID is get all categories
        public async Task<IActionResult> GetCategoriesSearchAsync(CategorySearchParamsDTO searchdetails)
        {
            // The following method is responsible for initiating a call to another method located in the "BankAccountEvents" file.
            string UserID = "U1";
            var response = await _categoryService.GetCategoriesSearchBySPAsync(searchdetails.searchString, UserID, searchdetails.userLatitude, searchdetails.userLongitude, searchdetails.distanceType,searchdetails.city, searchdetails.sectionID);
            return ResponseHandler.Handle(response);
        }
    }
}