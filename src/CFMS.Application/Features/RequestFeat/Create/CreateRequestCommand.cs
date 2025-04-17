using CFMS.Application.Common;
using CFMS.Application.DTOs.Request;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace CFMS.Application.Features.RequestFeat.Create
{
    public class CreateRequestCommand : IRequest<BaseResponse<bool>>
    {
        public CreateRequestCommand(bool isInventoryRequest, Guid? inventoryRequestTypeId, Guid? wareFromId, Guid? wareToId, List<InventoryRequestDetailDto>? inventoryDetails, TaskRequestDto? taskRequestRequest)
        {
            //RequestTypeId = requestTypeId;
            //Status = status;
            IsInventoryRequest = isInventoryRequest;
            InventoryRequestTypeId = inventoryRequestTypeId;
            WareFromId = wareFromId;
            WareToId = wareToId;
            InventoryDetails = inventoryDetails;
            TaskRequestRequest = taskRequestRequest;
        }

        //public Guid? RequestTypeId { get; set; }
        //public int? Status { get; set; }

        public bool IsInventoryRequest { get; set; }

        //InventoryRequest
        public Guid? InventoryRequestTypeId { get; set; }
        public Guid? WareFromId { get; set; }
        public Guid? WareToId { get; set; }
        public List<InventoryRequestDetailDto>? InventoryDetails { get; set; }

        //TaskRequest
        public TaskRequestDto? TaskRequestRequest { get; set; }

        //public CreateRequestCommand()
        //{
        //    InventoryDetails = new List<InventoryRequestDetailDto>();
        //}
    }
}
