using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenCoopFeat.Delete
{
    public class DeleteCoopCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteCoopCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
