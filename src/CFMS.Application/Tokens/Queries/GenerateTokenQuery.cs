using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Tokens.Queries
{
    public record GenerateTokenQuery(
        Guid? Id,
        string FirstName,
        string LastName,
        string Email,
        List<string> Roles) : IRequest<ErrorOr<GenerateTokenResult>>;
}
