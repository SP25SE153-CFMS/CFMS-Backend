using CFMS.Application.Features.FoodFeat.Create;
using CFMS.Application.Features.FoodFeat.GetFood;
using CFMS.Application.Features.FoodFeat.GetFoods;
using CFMS.Application.Features.FoodFeat.Update;
using CFMS.Application.Features.SupplierFeat.Create;
using CFMS.Application.Features.SupplierFeat.GetSupplier;
using CFMS.Application.Features.SupplierFeat.GetSuppliers;
using CFMS.Application.Features.SupplierFeat.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class FoodController : BaseController
    {
        public FoodController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetFoods()
        {
            var result = await Send(new GetFoodsQuery());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFood(Guid id)
        {
            var result = await Send(new GetFoodQuery(id));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFoodCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateFoodCommand command)
        {
            var result = await Send(command);
            return result;
        }
    }
}
