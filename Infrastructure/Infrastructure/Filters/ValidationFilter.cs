using Application.ResponceObject;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Twilio.TwiML.Messaging;

namespace Infrastructure.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                KeyValuePair<string, IEnumerable<string>>[] errors = context.ModelState
                    .Where(x => x.Value.Errors.Any())
                    .ToDictionary(e => e.Key, e => e.Value.Errors.Select(e => e.ErrorMessage))
                    .ToArray();

                context.Result = new BadRequestObjectResult(new Response(Application.ResponceObject.Enums.ResponseStatusCode.ValidationError)
                {
                    ValidationErrors = errors.Select(e => new CustomValidationError
                    {
                        PropertyName = e.Key,
                        ErrorMessage = string.Join("- ", e.Value)
                    }).ToList(),
                });
                return;
            }
            await next();
        }
    }
}
