using CFMS.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CFMS.Api.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;

        protected BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<IActionResult> Send<TResponse>(IRequest<TResponse> request)
        {
            var response = await _mediator.Send(request);
            var baseResponse = response as BaseResponse<TResponse>;

            if (IsSuccessful(baseResponse) == true) 
            {
                return Ok(baseResponse); 
            }

            var errorMessage = baseResponse?.Message ?? "Executed failed";
            return BadRequest(BaseResponse<TResponse>.FailureResponse(errorMessage));
        }

        public bool IsSuccessful<T>(BaseResponse<T> response)
        {
            return response != null && response.Success;
        }
    }
}
