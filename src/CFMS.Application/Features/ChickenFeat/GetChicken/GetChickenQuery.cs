using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.ChickenFeat.GetChicken
{
    public class GetChickenQuery : IRequest<BaseResponse<Chicken>>
    {
        public GetChickenQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
