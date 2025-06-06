﻿using CFMS.Application.Features.FarmFeat.AddFarmEmployee;
using CFMS.Application.Features.FarmFeat.Create;
using CFMS.Application.Features.FarmFeat.Delete;
using CFMS.Application.Features.FarmFeat.DeleteFarmEmployee;
using CFMS.Application.Features.FarmFeat.GetByGetFarmByCurrentUser;
using CFMS.Application.Features.FarmFeat.GetFarm;
using CFMS.Application.Features.FarmFeat.GetFarmByCurrentEmployeeByFarmId;
using CFMS.Application.Features.FarmFeat.GetFarmByCurrentUserId;
using CFMS.Application.Features.FarmFeat.GetFarmByUserId;
using CFMS.Application.Features.FarmFeat.GetFarmEmployee;
using CFMS.Application.Features.FarmFeat.GetFarmEmployeeByUserId;
using CFMS.Application.Features.FarmFeat.GetFarmEmployees;
using CFMS.Application.Features.FarmFeat.GetFarms;
using CFMS.Application.Features.FarmFeat.InviteEnrollFarm;
using CFMS.Application.Features.FarmFeat.Update;
using CFMS.Application.Features.FarmFeat.UpdateFarmEmployee;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class FarmController : BaseController
    {
        public FarmController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await Send(new GetFarmsQuery());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Send(new GetFarmQuery(id));
            return result;
        }

        [HttpGet("byUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var result = await Send(new GetFarmByUserIdQuery(userId));
            return result;
        }

        [HttpGet("currentUser")]
        public async Task<IActionResult> GetByGetFarmByCurrentUser()
        {
            var result = await Send(new GetByGetFarmByCurrentUserQuery());
            return result;
        }

        [HttpGet("currentEmployee")]
        public async Task<IActionResult> GetByGetFarmByCurrentEmployee()
        {
            var result = await Send(new GetFarmByCurrentEmployeeQuery());
            return result;
        }

        [HttpGet("currentEmployee/{farmId}")]
        public async Task<IActionResult> GetByGetFarmByCurrentEmployeeByFarmId(Guid farmId)
        {
            var result = await Send(new GetFarmByCurrentEmployeeByFarmIdQuery(farmId));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFarmCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateFarmCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Send(new DeleteFarmCommand(id));
            return result;
        }

        [HttpPost("add-employee")]
        public async Task<IActionResult> AddFarmEmployee(AddFarmEmployeeCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut("update-employee")]
        public async Task<IActionResult> UpdateFarmEmployee(UpdateFarmEmployeeCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("delete-employee")]
        public async Task<IActionResult> DeleteFarmEmployee(DeleteFarmEmployeeCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpGet("{id}/get-employees")]
        public async Task<IActionResult> GetFarmEmployees(Guid id)
        {
            var result = await Send(new GetFarmEmployeesQuery(id));
            return result;
        }

        [HttpGet("get-employee/{id}")]
        public async Task<IActionResult> GetFarmEmployee(Guid id)
        {
            var result = await Send(new GetFarmEmployeeQuery(id));
            return result;
        }

        [HttpGet("get-farm-employee/{userId}/{farmId}")]
        public async Task<IActionResult> GetFarmEmployeeByUserId(Guid userId, Guid farmId)
        {
            var result = await Send(new GetFarmEmployeeByUserIdQuery(userId, farmId));
            return result;
        }

        [HttpPost("invite-enroll")]
        public async Task<IActionResult> InviteEnrollFarm(InviteEnrollFarmCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPost("invite-enroll-decision")]
        public async Task<IActionResult> InviteEnrollDecisionFarm(InviteEnrollFarmDecisionCommand command)
        {
            var result = await Send(command);
            return result;
        }
    }
}
