using CFMS.Application.Common;
using CFMS.Application.DTOs.Request;
using MediatR;
using System;
using System.Collections.Generic;

namespace CFMS.Application.Features.RequestFeat.Update
{
    public class UpdateRequestCommand : IRequest<BaseResponse<bool>>
    {
        public Guid RequestId { get; set; }
        public Guid? RequestTypeId { get; set; }
        public int? Status { get; set; }
        public bool IsInventoryRequest { get; set; }

        // Inventory Request
        public Guid? InventoryRequestTypeId { get; set; }
        public Guid? WareFromId { get; set; }
        public Guid? WareToId { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public List<InventoryRequestDetailDto>? InventoryDetails { get; set; }

        // Task Request
        public Guid? TaskTypeId { get; set; }
        public int? Priority { get; set; }
        public string? Description { get; set; }

        public UpdateRequestCommand()
        {
            InventoryDetails = new List<InventoryRequestDetailDto>();
        }
    }
}
