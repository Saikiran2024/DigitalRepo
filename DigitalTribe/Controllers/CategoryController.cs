using DigitalTribe.Helpers;
using DTribe.Core.DTO;
using DTribe.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalTribe.Controllers
{
    [Authorize]
    [Route("api/Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public readonly ICategoriesService _categoryService;
        public CategoryController(ICategoriesService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("UserPostedList")] 
        public async Task<IActionResult> UserPostedList(CategorySearchParamsDTO searchdetails)
        {
            var response = await _categoryService.GetPostedListBySearch(searchdetails.searchString,searchdetails.distanceType, searchdetails.sectionID);
            return ResponseHandler.Handle(response);
        }

        //[HttpGet("UserPostedList")] 
        //public async Task<IActionResult> GetPostedList()
        //{
        //    var response = await _categoryService.GetPostedList();
        //    return ResponseHandler.Handle(response);
        //}
    }
}