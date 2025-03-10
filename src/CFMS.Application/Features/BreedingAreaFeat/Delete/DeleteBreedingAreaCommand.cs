using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.BreedingAreaFeat.Delete
{
    public class DeleteBreedingAreaCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteBreedingAreaCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
