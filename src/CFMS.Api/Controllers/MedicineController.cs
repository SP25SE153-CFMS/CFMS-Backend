using CFMS.Application.Features.FoodFeat.Create;
using CFMS.Application.Features.FoodFeat.Delete;
using CFMS.Application.Features.FoodFeat.GetFood;
using CFMS.Application.Features.FoodFeat.GetFoods;
using CFMS.Application.Features.FoodFeat.Update;
using CFMS.Application.Features.MedicineFeat.Create;
using CFMS.Application.Features.MedicineFeat.Delete;
using CFMS.Application.Features.MedicineFeat.GetMedicine;
using CFMS.Application.Features.MedicineFeat.GetMedicines;
using CFMS.Application.Features.MedicineFeat.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class MedicineController : BaseController
    {
        public MedicineController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetMedicines()
        {
            var result = await Send(new GetMedicinesQuery());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicine(Guid id)
        {
            var result = await Send(new GetMedicineQuery(id));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMedicineCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateMedicineCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteMedicineCommand command)
        {
            var result = await Send(command);
            return result;
        }
    }
}
