using AutoMapper;
using CFMS.Application.DTOs.Receipt;
using CFMS.Application.Features.ShiftFeat.Create;
using CFMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Mappings
{
    public class ReceiptProfile : Profile
    {
        public ReceiptProfile()
        {
            CreateMap<InventoryReceipt, ReceiptResponse>()
                .ForMember(dest => dest.WareFromId, opt => opt.Ignore())
                .ForMember(dest => dest.WareToId, opt => opt.Ignore());
        }
    }
}
