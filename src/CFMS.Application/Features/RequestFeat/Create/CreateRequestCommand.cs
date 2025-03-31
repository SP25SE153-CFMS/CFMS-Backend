using CFMS.Application.Common;
using CFMS.Application.DTOs.Request;
using MediatR;
using System;
using System.Collections.Generic;

namespace CFMS.Application.Features.RequestFeat.Create
{
    public class CreateRequestCommand : IRequest<BaseResponse<bool>>
    {
        public Guid? RequestTypeId { get; set; }
        public int? Status { get; set; }

        public bool IsInventoryRequest { get; set; }

        //InventoryRequest
        public Guid? InventoryRequestTypeId { get; set; }
        public Guid? WareFromId { get; set; }
        public Guid? WareToId { get; set; }
        public List<InventoryRequestDetailDto>? InventoryDetails { get; set; }

        //TaskRequest
        public Guid? TaskTypeId { get; set; }
        public int? Priority { get; set; }
        public string? Description { get; set; }

        public CreateRequestCommand()
        {
            InventoryDetails = new List<InventoryRequestDetailDto>();
        }
    }
}
