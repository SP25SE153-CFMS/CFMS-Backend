﻿using AutoMapper;
using CFMS.Application.DTOs.ChickenBatch;
using CFMS.Application.Features.ChickenBatchFeat.Create;
using CFMS.Application.Features.ChickenBatchFeat.OpenChickenBatch;
using CFMS.Application.Features.ChickenBatchFeat.SplitChickenBatch;
using CFMS.Domain.Entities;

namespace CFMS.Application.Mappings
{
    public class ChickenBatchProfile : Profile
    {
        public ChickenBatchProfile()
        {
            CreateMap<CreateChickenBatchCommand, ChickenBatch>();
            CreateMap<OpenChickenBatchCommand, ChickenBatch>();
            CreateMap<SplitChickenBatchCommand, ChickenBatch>();
            CreateMap<ChickenBatch, ChickenBatchResponse>();
        }
    }
}
