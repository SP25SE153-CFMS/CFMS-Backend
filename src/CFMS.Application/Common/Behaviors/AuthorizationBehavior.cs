using CFMS.Application.Common.Interfaces;
using CFMS.Application.Common.Security.Request;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Common.Behaviors
{
    public class AuthorizationBehavior<TRequest, TResponse>(
        IAuthorizationService _authorizationService)
            : IPipelineBehavior<TRequest, TResponse>
                where TRequest : IAuthorizeableRequest<TResponse>
                where TResponse : IErrorOr
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var authorizationAttributes = request.GetType()
                .GetCustomAttributes<AuthorizeAttribute>()
                .ToList();

            if (authorizationAttributes.Count == 0)
            {
                return await next();
            }

            var requiredRoles = authorizationAttributes
                .SelectMany(authorizationAttribute => authorizationAttribute.Roles?.Split(',') ?? [])
                .ToList();

            var authorizationResult = _authorizationService.AuthorizeCurrentUser(
                request,
                requiredRoles);

            return authorizationResult.IsError
                ? (dynamic)authorizationResult.Errors
                : await next();
        }
    }
}
