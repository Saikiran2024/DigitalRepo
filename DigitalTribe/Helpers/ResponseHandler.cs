using DTribe.Core.ResponseObjects;
using Microsoft.AspNetCore.Mvc;

namespace DigitalTribe.Helpers
{
    public class ResponseHandler
    {
        public static IActionResult Handle<T>(StandardResponse<T> response)
        {
            switch (response.Status)
            {
                case ResponseStatus.Success:
                case ResponseStatus.Error:
                    return new OkObjectResult(response);

                case ResponseStatus.Fatal:
                default:
                    return new ObjectResult(new { message = "Unknown error occurred" }) { StatusCode = 500 };
            }
        }
    }
}
