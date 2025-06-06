﻿using CFMS.Application.Common;
using CFMS.Application.DTOs.Request;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace CFMS.Application.Features.RequestFeat.Create
{
    public class CreateRequestCommand : IRequest<BaseResponse<bool>>
    {
        public CreateRequestCommand(bool isInventoryRequest, Guid? inventoryRequestTypeId, Guid? wareFromId, Guid? wareToId, List<InventoryRequestDetailDto>? inventoryDetails, TaskRequestDto? taskRequestRequest, string? reason, string? note, DateTime? expectedDate)
        {
            //RequestTypeId = requestTypeId;
            //Status = status;
            IsInventoryRequest = isInventoryRequest;
            InventoryRequestTypeId = inventoryRequestTypeId;
            WareFromId = wareFromId;
            WareToId = wareToId;
            Reason = reason;
            Note = note;
            InventoryDetails = inventoryDetails;
            TaskRequestRequest = taskRequestRequest;
            ExpectedDate = expectedDate;
        }

        //public Guid? RequestTypeId { get; set; }
        //public int? Status { get; set; }

        public bool IsInventoryRequest { get; set; }

        //InventoryRequest
        public Guid? InventoryRequestTypeId { get; set; }
        public Guid? WareFromId { get; set; }
        public Guid? WareToId { get; set; }
        public string? Reason { get; set; }
        public string? Note { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public List<InventoryRequestDetailDto>? InventoryDetails { get; set; }

        //TaskRequest
        public TaskRequestDto? TaskRequestRequest { get; set; }

        //public CreateRequestCommand()
        //{
        //    InventoryDetails = new List<InventoryRequestDetailDto>();
        //}
    }
}
