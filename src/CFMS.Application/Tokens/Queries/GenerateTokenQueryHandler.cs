using CFMS.Application.Common.Interfaces;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Tokens.Queries
{
    public class GenerateTokenQueryHandler(
        IJwtTokenGenerator _jwtTokenGenerator)
            : IRequestHandler<GenerateTokenQuery, ErrorOr<GenerateTokenResult>>
    {
        public Task<ErrorOr<GenerateTokenResult>> Handle(GenerateTokenQuery query, CancellationToken cancellationToken)
        {
            var id = query.Id ?? Guid.NewGuid();

            var token = _jwtTokenGenerator.GenerateToken(
                id,
                query.FirstName,
                query.LastName,
                query.Email,
                query.Roles);

            var authResult = new GenerateTokenResult(
                id,
                query.FirstName,
                query.LastName,
                query.Email,
                token);

            return Task.FromResult(ErrorOrFactory.From(authResult));
        }
    }
}
