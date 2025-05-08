using AutoMapper;
using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.AddHealthLog
{
    public class AddHealthLogCommandHandler : IRequestHandler<AddHealthLogCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddHealthLogCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(AddHealthLogCommand request, CancellationToken cancellationToken)
        {
            var existBatch = _unitOfWork.ChickenBatchRepository.Get(filter: b => b.ChickenBatchId.Equals(request.ChickenBatchId) && b.IsDeleted == false).FirstOrDefault();
            if (existBatch == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Lứa không tồn tại");
            }

            var existTask = _unitOfWork.TaskRepository.Get(filter: t => t.TaskId.Equals(request.TaskId) && t.IsDeleted == false).FirstOrDefault();
            if (existBatch == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Công việc không tồn tại");
            }

            try
            {
                var healthLog = _mapper.Map<HealthLog>(request);

                foreach (var healthLogDetail in request.HealthLogDetails)
                {
                    healthLog.HealthLogDetails.Add(new HealthLogDetail
                    {
                        CriteriaId = healthLogDetail.CriteriaId,
                        Result = healthLogDetail.Result,
                    });
                }

                _unitOfWork.HealthLogRepository.Insert(healthLog);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Thêm thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Thêm ko thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
