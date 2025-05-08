using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenFeat.Delete
{
    public class DeleteChickenCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteChickenCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
