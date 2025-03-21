using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenFeat.UpdateChickenDetail
{
    public class UpdateChickenDetailCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateChickenDetailCommand(Guid chickenDetailId, Guid? chickenId, decimal? weight, int? quantity, int? gender)
        {
            ChickenDetailId = chickenDetailId;
            ChickenId = chickenId;
            Weight = weight;
            Quantity = quantity;
            Gender = gender;
        }

        public Guid ChickenDetailId { get; set; }

        public Guid? ChickenId { get; set; }

        public decimal? Weight { get; set; }

        public int? Quantity { get; set; }

        public int? Gender { get; set; }
    }
}
