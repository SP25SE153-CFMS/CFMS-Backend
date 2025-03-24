using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenFeat.AddChickenDetail
{
    public class AddChickenDetailCommandHandler : IRequestHandler<AddChickenDetailCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddChickenDetailCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(AddChickenDetailCommand request, CancellationToken cancellationToken)
        {
            var existChicken = _unitOfWork.ChickenRepository.Get(filter: c => c.ChickenId.Equals(request.ChickenId) && c.IsDeleted == false).FirstOrDefault();
            if (existChicken == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Gà không tồn tại");
            }

            try
            {
                existChicken.ChickenDetails.Add(new ChickenDetail
                {
                    ChickenId = request.ChickenId,
                    Gender = request.Gender,
                    Weight = request.Weight,
                    Quantity = request.Quantity,
                });

                _unitOfWork.ChickenRepository.Update(existChicken);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Thêm thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Thêm không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
