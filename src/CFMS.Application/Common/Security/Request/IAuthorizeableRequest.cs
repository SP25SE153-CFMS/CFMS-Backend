using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Common.Security.Request
{
    public interface IAuthorizeableRequest<T> : IRequest<T>
    {
        Guid UserId { get; }
    }
}
