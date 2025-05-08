using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.Delete
{
    public class DeleteChickenBatchCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteChickenBatchCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
