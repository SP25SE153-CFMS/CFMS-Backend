using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.RequestFeat.CreateReceipt
{
    using global::CFMS.Application.Common;
    using global::CFMS.Application.Features.InventoryReceipts.Commands;
    using global::CFMS.Domain.Entities;
    using global::CFMS.Domain.Enums.Types;
    using global::CFMS.Domain.Interfaces;
    using MediatR;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    namespace CFMS.Application.Features.InventoryReceipts.Commands
    {
        public class CreateInventoryReceiptCommandHandler : IRequestHandler<CreateInventoryReceiptCommand, BaseResponse<bool>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CreateInventoryReceiptCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<BaseResponse<bool>> Handle(CreateInventoryReceiptCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var existRequest = _unitOfWork.RequestRepository.Get(filter: x => x.RequestId.Equals(request.RequestId) && x.IsDeleted == false).FirstOrDefault();
                    if (existRequest == null)
                    {
                        return BaseResponse<bool>.FailureResponse("Phiếu yêu cầu không tồn tại");
                    }

                    if (existRequest.Status.Equals(1))
                    {
                        return BaseResponse<bool>.FailureResponse("Phiếu yêu cầu đã bị từ chối");
                    }

                    if (existRequest.Status.Equals(0))
                    {
                        return BaseResponse<bool>.FailureResponse("Phiếu yêu cầu đang chờ duyệt");
                    }

                    var existReceiptType = _unitOfWork.SubCategoryRepository.Get(filter: x => x.SubCategoryId.Equals(request.ReceiptTypeId) && x.IsDeleted == false).FirstOrDefault();

                    if (existReceiptType == null)
                    {
                        return BaseResponse<bool>.FailureResponse("Loại phiếu không tồn tại");
                    }



                    //var inventoryReceipt = new InventoryReceipt
                    //{
                    //    InventoryRequestId = request.InventoryRequestId,
                    //    ReceiptTypeId = request.ReceiptTypeId,
                    //    ReceiptCodeNumber = $"PNX-{DateTime.UtcNow.Ticks}",
                    //    Status = 1,
                    //    InventoryReceiptDetails = request.ReceiptDetails.Select(d => new InventoryReceiptDetail
                    //    {
                    //        InventoryReceiptDetailId = Guid.NewGuid(),
                    //        ActualQuantity = d.ActualQuantity,
                    //        ActualDate = d.ActualDate,
                    //        Note = d.Note,
                    //        BatchNumber = d.BatchNumber
                    //    }).ToList()
                    //};

                    //await _receiptRepository.Insert(inventoryReceipt);
                    //await _receiptRepository.Save();

                    //inventoryReceipt.InventoryReceiptId;

                    return BaseResponse<bool>.SuccessResponse("Tạo phiếu nhập thành công");
                }
                catch (Exception ex)
                {
                    return BaseResponse<bool>.FailureResponse("Tạo thất bại: " + ex.Message);
                }
            }
        }
    }
}
