using CFMS.Application.Common.Security.Request;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CFMS.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class ApiController : ControllerBase
    {
        protected ActionResult Problem(List<Error> errors)
        {
            if (!errors.Any())
            {
                return Problem();  
            }

            return errors.All(e => e.Type == ErrorType.Validation)
                ? HandleValidationErrors(errors) 
                : HandleSpecificError(errors.First()); 
        }

        private ObjectResult HandleSpecificError(Error error)
        {
            var statusCode = error.Type switch
            {
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Unauthorized => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError,
            };

            return Problem(statusCode: statusCode, title: error.Description);
        }

        private ActionResult HandleValidationErrors(List<Error> errors)
        {
            var modelState = new ModelStateDictionary();
            errors.ForEach(error => modelState.AddModelError(error.Code, error.Description));

            return ValidationProblem(modelState);
        }
    }
}
