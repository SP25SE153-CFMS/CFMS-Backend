using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.GrowthStageFeat.Delete
{
    public class DeleteStageCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteStageCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
