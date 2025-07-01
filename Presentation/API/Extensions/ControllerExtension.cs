using Application.ResponceObject;
using Application.ResponceObject.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Api.Extensions
{
    public static class ControllerExtension
    {
        public static IActionResult HandleResponse(this ControllerBase controller, Response response)
        {
            return response.ResponseStatusCode switch
            {
                ResponseStatusCode.Error => controller.BadRequest(response),
                ResponseStatusCode.ValidationError => controller.BadRequest(response),
                ResponseStatusCode.NotFound => controller.NotFound(response),
                ResponseStatusCode.Forbidden => controller.Forbid(),
                _ => controller.Ok(response)
            };
        }
    }
}
