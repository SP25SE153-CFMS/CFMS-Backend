using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Features.ShiftFeat.GetShifts;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.RequestFeat.GetReceipts
{
    public class GetReceiptsQueryHandler : IRequestHandler<GetReceiptsQuery, BaseResponse<IEnumerable<InventoryReceipt>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetReceiptsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<InventoryReceipt>>> Handle(GetReceiptsQuery request, CancellationToken cancellationToken)
        {
            var existReceipt = _unitOfWork.InventoryReceiptRepository.GetIncludeMultiLayer(filter: f => f.IsDeleted == false,
                include: x => x
                .Include(r => r.InventoryReceiptDetails),
                orderBy: q => q.OrderByDescending(x => x.CreatedWhen)
                ).ToList();

            if (existReceipt == null)
            {
                return BaseResponse<IEnumerable<InventoryReceipt>>.SuccessResponse(message: "Phiếu nhập không tồn tại");
            }
            return BaseResponse<IEnumerable<InventoryReceipt>>.SuccessResponse(_mapper.Map<IEnumerable<InventoryReceipt>>(existReceipt));
        }
    }
}
