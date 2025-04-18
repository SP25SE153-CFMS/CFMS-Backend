using CFMS.Application.Features.FoodFeat.Create;
using CFMS.Application.Features.FoodFeat.Delete;
using CFMS.Application.Features.FoodFeat.GetFood;
using CFMS.Application.Features.FoodFeat.GetFoods;
using CFMS.Application.Features.FoodFeat.Update;
using CFMS.Application.Features.InventoryReceipts.Commands;
using CFMS.Application.Features.RequestFeat.ApproveRequest;
using CFMS.Application.Features.RequestFeat.Create;
using CFMS.Application.Features.RequestFeat.Delete;
using CFMS.Application.Features.RequestFeat.GetRequest;
using CFMS.Application.Features.RequestFeat.GetRequestByCurrentUser;
using CFMS.Application.Features.RequestFeat.GetRequestByFarmId;
using CFMS.Application.Features.RequestFeat.GetRequests;
using CFMS.Application.Features.RequestFeat.Update;
using CFMS.Application.Features.RequestFeat.UploadImage;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class RequestController : BaseController
    {
        public RequestController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetRequests()
        {
            var result = await Send(new GetRequestsQuery());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequest(Guid id)
        {
            var result = await Send(new GetRequestQuery(id));
            return result;
        }

        [HttpGet("byCurrentUser")]
        public async Task<IActionResult> GetRequestByCurrentUser()
        {
            var result = await Send(new GetRequestByCurrentUserQuery());
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRequestCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateRequestCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Send(new DeleteRequestCommand(id));
            return result;
        }

        [HttpPut("/approve")]
        public async Task<IActionResult> Approve(ApproveRequestCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPost("/create-inventory-receipt")]
        public async Task<IActionResult> CreateInventoryReceipt(CreateInventoryReceiptCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(UploadImageCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpGet("Farm/{farmId}")]
        public async Task<IActionResult> GetRequestByFarmId(Guid farmId)
        {
            var result = await Send(new GetRequestByFarmIdQuery(farmId));
            return result;
        }
    }
}
