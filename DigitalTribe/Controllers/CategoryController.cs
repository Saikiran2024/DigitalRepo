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
        [HttpGet("{CategoryID}")]
        public async Task<IActionResult> GeCategoryDetailsByClientId(string CategoryID)
        {
            // The following method is responsible for initiating a call to another method located in the "BankAccountEvents" file.
            var response = await _categoryService.GetCategoryList(CategoryID);
            return ResponseHandler.Handle(response);
        }

        // This section of code is responsible for insert kycBankAccount details for multiple clients.
        [HttpPost("Add")]
        public async Task<IActionResult> InsertCategoryDetails(string userID, CategoriesDTO details)
        {
            var response = await _categoryService.InsertCategory(userID, details);
            return ResponseHandler.Handle(response);
        }

        //// This section of code is responsible for update kycBankAccount details for multiple clients.
        //[HttpPost("Update")]
        //public async Task<IActionResult> UpdateBankAccountDetails(string tenantID, string userID, List<BankAccountDTO> details)
        //{
        //    var response = await _bankaccountEvents.UpdateBankAccountDetails(tenantID, userID, details);
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
