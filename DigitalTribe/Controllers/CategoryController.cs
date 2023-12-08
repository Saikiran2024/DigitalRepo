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
        public readonly ICategoryService _categoryService;
        public CategoryController (ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("GlobalCategories")]
        public async Task<IActionResult> GeGlobalCategoryList()
        {
            // The following method is responsible for initiating a call to another method located in the "" file.
            var response = await _categoryService.GetGlobalCategoryList();
            return ResponseHandler.Handle(response);
        }
        [HttpGet("GlobalCategory/Items/{CategoryID}")]
        public async Task<IActionResult> GeGlobalCategoryItemList(string CategoryID)
        {
            // The following method is responsible for initiating a call to another method located in the "BankAccountEvents" file.
            var response = await _categoryService.GetGlobalCategoryItemList(CategoryID);
            return ResponseHandler.Handle(response);
        }
        [HttpGet("/User/Item/{CategoryID}")]
        public async Task<IActionResult> GetUserWiseCategoryItemList(string CategoryID)
        {
            string userID = "U1";
            // The following method is responsible for initiating a call to another method located in the "BankAccountEvents" file.
            var response = await _categoryService.GetUserCategoryItemList(userID, CategoryID);
            return ResponseHandler.Handle(response);
        }

        [HttpPost("Item/Add")]
        public async Task<IActionResult> InsertCategoryDetails(List<CategoryItemDTO> details)
        {
            string userID = "U1";
            var response = await _categoryService.InsertCategoryItem(userID, details);
            return ResponseHandler.Handle(response);
        }

        [HttpPost("Item/Delete")]
        public async Task<IActionResult> DeleteCategoryItemDetails(List<CategoryItemDTO> details)
        {
            string userID = "U1";
            var response = await _categoryService.DeleteCategoryItem(userID, details);
            return ResponseHandler.Handle(response);
        }


        // This section of code is responsible for insert kycBankAccount details for multiple clients.
        //[HttpPost("Add")]
        //public async Task<IActionResult> InsertCategoryDetails(List<CategoriesDTO> details)
        //{
        //    string userID = "U1";
        //    var response = await _categoryService.InsertCategory(userID, details);
        //    return ResponseHandler.Handle(response);
        //}

        // This section of code is responsible for update kycBankAccount details for multiple clients.
        //[HttpPost("Update")]
        //public async Task<IActionResult> UpdateCategoryDetails(List<CategoriesDTO> details)
        //{
        //    string userID = "U1";
        //    var response = await _categoryService.UpdateCategory( userID, details);
        //    return ResponseHandler.Handle(response);
        //}

        //// This section of code is responsible for delete kycBankAccount details for multiple clients.
        //[HttpPost("Delete")]
        //public async Task<IActionResult> DeleteBankAccountDetails(string tenantID, string userID, List<BankAccountDTO> details)
        //{
        //    var response = await _bankaccountEvents.DeleteBankAccountDetails(tenantID, userID, details);
        //    return ResponseHandler.Handle(response);
        //}
    }
}
