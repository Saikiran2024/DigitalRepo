using DigitalTribe.Helpers;
using DTribe.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalTribe.Controllers
{
    [Route("api/Sections")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        public readonly ISectionService _sectionService;
        public SectionController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetSectionList()
        {
            var response = await _sectionService.GetSectionList();
            return ResponseHandler.Handle(response);
        }

        [HttpGet("Categories/List/{SectionID}")]
        public async Task<IActionResult> GetSectionCategoriesList(string SectionID)
        {
            var response = await _sectionService.GetSectionCategoryList(SectionID);
            return ResponseHandler.Handle(response);
        }
    }
}
