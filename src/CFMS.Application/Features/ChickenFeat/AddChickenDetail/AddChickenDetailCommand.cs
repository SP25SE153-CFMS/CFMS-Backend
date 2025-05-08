using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenFeat.AddChickenDetail
{
    public class AddChickenDetailCommand : IRequest<BaseResponse<bool>>
    {
        public AddChickenDetailCommand(Guid? chickenId, decimal? weight, int? quantity, int? gender)
        {
            ChickenId = chickenId;
            Weight = weight;
            Quantity = quantity;
            Gender = gender;
        }

        public Guid? ChickenId { get; set; }

        public decimal? Weight { get; set; }

        public int? Quantity { get; set; }

        public int? Gender { get; set; } //0-Male, 1-Female
    }
}
