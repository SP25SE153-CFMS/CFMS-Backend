using CFMS.Application.Common.Security.Request;
using CFMS.Domain.Core.Bus;
using CFMS.Domain.Core.Notifications;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CFMS.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class ApiController : ControllerBase
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediator;

        public ApiController(IMediatorHandler mediator, DomainNotificationHandler notifications)
        {
            _mediator = mediator;
            _notifications = notifications;
        }

        protected IEnumerable<DomainNotification> Notifications => _notifications.GetNotifications();

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


        protected bool IsValidOperation()
        {
            return !_notifications.HasNotifications();
        }

        protected new IActionResult Response(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result,
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifications.GetNotifications().Select(n => n.Value),
            });
        }

        protected void NotifyModelStateErrors()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(string.Empty, erroMsg);
            }
        }

        protected void NotifyError(string code, string message)
        {
            _mediator.RaiseEvent(new DomainNotification(code, message));
        }

        protected void AddIdentityErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                NotifyError(result.ToString(), error.Description);
            }
        }
    }
}
