using CFMS.Application.Tokens.Queries;
using CFMS.Contracts.Tokens;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    [Route("tokens")]
    [AllowAnonymous]
    public class TokensController(ISender _mediator) : ApiController
    {
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateToken(GenerateTokenRequest request)
        {
            var query = new GenerateTokenQuery(
                request.Id,
                request.FirstName,
                request.LastName,
                request.Email,
                request.Roles);

            var result = await _mediator.Send(query);

            return result.Match(
                generateTokenResult => Ok(ToDto(generateTokenResult)),
                Problem);
        }

        private static TokenResponse ToDto(GenerateTokenResult authResult)
        {
            return new TokenResponse(
                authResult.Id,
                authResult.FirstName,
                authResult.LastName,
                authResult.Email,
                authResult.Token);
        }
    }
}
