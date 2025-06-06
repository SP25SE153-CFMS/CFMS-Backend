﻿using CFMS.Application.Features.ChickenCoopFeat.AddCoopEquipment;
using CFMS.Application.Features.ChickenCoopFeat.Create;
using CFMS.Application.Features.ChickenCoopFeat.Delete;
using CFMS.Application.Features.ChickenCoopFeat.DeleteCoopEquipment;
using CFMS.Application.Features.ChickenCoopFeat.GetCoop;
using CFMS.Application.Features.ChickenCoopFeat.GetCoops;
using CFMS.Application.Features.ChickenCoopFeat.Update;
using CFMS.Application.Features.ChickenCoopFeat.UpdateCoopEquipment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class ChickenCoopController : BaseController
    {
        public ChickenCoopController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("BreedingArea/{breedingAreaId}")]
        public async Task<IActionResult> GetCoops(Guid breedingAreaId)
        {
            var result = await Send(new GetCoopsQuery(breedingAreaId));
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoop(Guid id)
        {
            var result = await Send(new GetCoopQuery(id));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCoopCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCoopCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Send(new DeleteCoopCommand(id));
            return result;
        }

        [HttpPost("add-coopequipment")]
        public async Task<IActionResult> AddCoopEquipment(AddCoopEquipmentCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut("update-coopequipment")]
        public async Task<IActionResult> UpdateCoopEquipment(UpdateCoopEquipmentCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("delete-coopequipment")]
        public async Task<IActionResult> DeleteCoopEquipment(DeleteCoopEquipmentCommand command)
        {
            var result = await Send(command);
            return result;
        }
    }
}
