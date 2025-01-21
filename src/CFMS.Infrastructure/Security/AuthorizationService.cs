using CFMS.Application.Common.Security.Request;
using CFMS.Infrastructure.Security.CurrentUserProvider;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using CFMS.Application.Common.Interfaces;

namespace CFMS.Infrastructure.Security
{
    public class AuthorizationService(
        ICurrentUserProvider _currentUserProvider)
            : IAuthorizationService
    {
        public ErrorOr<Success> AuthorizeCurrentUser<T>(
            IAuthorizeableRequest<T> request,
            List<string> requiredRoles)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();

            if (requiredRoles.Except(currentUser.Roles).Any())
            {
                return ErrorOr.Error.Unauthorized(description: "User is no permission.");
            }

            return Result.Success;
        }
    }
}
