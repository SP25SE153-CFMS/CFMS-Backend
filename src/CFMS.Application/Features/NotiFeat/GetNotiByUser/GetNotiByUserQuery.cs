using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.NotiFeat.GetNotiByUser
{
    public class GetNotiByUserQuery : IRequest<BaseResponse<IEnumerable<Notification>>>
    {
        public GetNotiByUserQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }
}
