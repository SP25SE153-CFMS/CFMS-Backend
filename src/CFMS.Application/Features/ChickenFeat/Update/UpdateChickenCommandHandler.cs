using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenFeat.Update
{
    public class UpdateChickenCommandHandler : IRequestHandler<UpdateChickenCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateChickenCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateChickenCommand request, CancellationToken cancellationToken)
        {
            var existChicken = _unitOfWork.ChickenRepository.Get(filter: c => c.ChickenId.Equals(request.Id) && c.IsDeleted == false).FirstOrDefault();
            if (existChicken == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Gà không tồn tại");
            }

            var existNameCode = _unitOfWork.ChickenRepository.Get(c => c.ChickenCode.Equals(request.ChickenCode) || c.ChickenName.Equals(request.ChickenName) && c.IsDeleted == false).FirstOrDefault();
            if (existNameCode != null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Tên hoặc mã loại gà đã được sử dụng");
            }

            try
            {
                existChicken.ChickenCode = request.ChickenCode;
                existChicken.ChickenName = request.ChickenName;
                existChicken.TotalQuantity = request.TotalQuantity;
                existChicken.Status = request.Status;
                existChicken.Description = request.Description;
                existChicken.ChickenTypeId = request.ChickenTypeId;

                var chickenDetails = request.ChickenDetails.Select(detail => new ChickenDetail
                {
                    ChickenId = existChicken.ChickenId,
                    Weight = detail.Weight,
                    Quantity = detail.Quantity,
                    Gender = detail.Gender
                }).ToList() ?? new List<ChickenDetail>();

                var existingDetails = _unitOfWork.ChickenDetailRepository
                      .Get(filter: d => d.ChickenId == existChicken.ChickenId)
                      .ToList();

                _unitOfWork.ChickenDetailRepository.DeleteRange(existingDetails);
                await _unitOfWork.SaveChangesAsync();

                existChicken.ChickenDetails.Clear();
                existChicken.ChickenDetails = chickenDetails;

                _unitOfWork.ChickenRepository.Update(existChicken);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Cập nhật không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
