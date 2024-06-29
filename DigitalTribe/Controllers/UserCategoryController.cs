using DigitalTribe.Helpers;
using DTribe.Core.DTO;
using DTribe.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalTribe.Controllers
{
    [Route("api/UserCategory")]
    [ApiController]
    public class UserCategoryController : ControllerBase
    {
        public readonly IUserCategoryService _UsercategoryService;
        public UserCategoryController (IUserCategoryService usercategoryService)
        {
            _UsercategoryService = usercategoryService;
        }


        [HttpGet("List/{sectionID}")]  //if sectionID is get all categories
        public async Task<IActionResult> GetUserCategoryDetails(string sectionID)
        {
            // The following method is responsible for initiating a call to another method located in the "BankAccountEvents" file.
            string UserID = "U1";
            var response = await _UsercategoryService.GetUserCategoriesListAsync(UserID,sectionID);
            return ResponseHandler.Handle(response);
        }

        [HttpGet("{Uscid}")]
        public async Task<IActionResult> GetCategoryDetailsByIDX(string Uscid)
        {
            // The following method is responsible for initiating a call to another method located in the "BankAccountEvents" file.
            var response = await _UsercategoryService.GetCategoryDetailsByIDX(Uscid);
            return ResponseHandler.Handle(response);
        }

        [HttpGet("Search/{SerachString}/{sectionID}")]
        public async Task<IActionResult> GetCategoryDetailsByIDX(string SerachString,string sectionID)
        {
            // The following method is responsible for initiating a call to another method located in the "BankAccountEvents" file.
            string UserID = "U1";
            var response = await _UsercategoryService.CategoryWiseSearch(UserID, SerachString, sectionID);
            return ResponseHandler.Handle(response);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> InsertCategoryDetails(UserCategoriesDTO details)
        {
            string userID = "U1";
            var response = await _UsercategoryService.Insert(userID, details);
            return ResponseHandler.Handle(response);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateCategoryDetails(UserCategoriesDTO details)
        {
            string userID = "U1";
            var response = await _UsercategoryService.Update(userID, details);
            return ResponseHandler.Handle(response);

        }

        [HttpDelete("Delete/{USCID}")]
        public async Task<IActionResult> DeleteCategoryDetails(string USCID)
        {
            string userID = "U1";
            var response = await _UsercategoryService.Delete(userID, USCID);
            return ResponseHandler.Handle(response);
        }

    }
}
