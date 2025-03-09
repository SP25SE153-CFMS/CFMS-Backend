using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.FarmFeat.Delete
{
    public class DeleteFarmCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteFarmCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
