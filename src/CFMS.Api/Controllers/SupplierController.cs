﻿using CFMS.Application.Features.SupplierFeat.AddResourceSupplier;
using CFMS.Application.Features.SupplierFeat.Create;
using CFMS.Application.Features.SupplierFeat.Delete;
using CFMS.Application.Features.SupplierFeat.DeleteResourceSupplier;
using CFMS.Application.Features.SupplierFeat.GetResourceSuppliers;
using CFMS.Application.Features.SupplierFeat.GetSupplier;
using CFMS.Application.Features.SupplierFeat.GetSupplierByResourceId;
using CFMS.Application.Features.SupplierFeat.GetSupplierByResourceSupplierId;
using CFMS.Application.Features.SupplierFeat.GetSuppliers;
using CFMS.Application.Features.SupplierFeat.GetSuppliersByFarmId;
using CFMS.Application.Features.SupplierFeat.Update;
using CFMS.Application.Features.SupplierFeat.UpdateResourceSupplier;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Api.Controllers
{
    public class SupplierController : BaseController
    {
        public SupplierController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetSuppliers()
        {
            var result = await Send(new GetSuppliersQuery());
            return result;
        }        
        
        [HttpGet("byFarmId/{farmId}")]
        public async Task<IActionResult> GetSuppliers(Guid farmId)
        {
            var result = await Send(new GetSuppliersByFarmIdQuery(farmId));
            return result;
        }

        [HttpGet("resource-suppliers/{supplierId}")]
        public async Task<IActionResult> GetResourceSuppliers(Guid supplierId)
        {
            var result = await Send(new GetResourceSuppliersQuery(supplierId));
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplier(Guid id)
        {
            var result = await Send(new GetSupplierQuery(id));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSupplierCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateSupplierCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Send(new DeleteSupplierCommand(id));
            return result;
        }

        [HttpPost("add-resource-supplier")]
        public async Task<IActionResult> AddResourceSupplier(AddResourceSupplierCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut("update-resource-supplier")]
        public async Task<IActionResult> UpdateResourceSupplier(UpdateResourceSupplierCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("delete-resource-supplier/{resourceSupplierId}")]
        public async Task<IActionResult> DeleteResourceSupplier(Guid resourceSupplierId)
        {
            var result = await Send(new DeleteResourceSupplierCommand(resourceSupplierId));
            return result;
        }

        [HttpGet("by-resource/{resourceId}")]
        public async Task<IActionResult> GetSupplierByResourceId(Guid resourceId)
        {
            var result = await Send(new GetSupplierByResourceIdQuery(resourceId));
            return result;
        }

        [HttpGet("by-resource-supplier/{resourceSupplierId}")]
        public async Task<IActionResult> GetSupplierByResourceSupplierId(Guid resourceSupplierId)
        {
            var result = await Send(new GetSupplierByResourceSupplierIdQuery(resourceSupplierId));
            return result;
        }
    }
}
