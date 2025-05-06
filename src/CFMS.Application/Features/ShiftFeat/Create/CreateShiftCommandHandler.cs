using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Features.FoodFeat.Create;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.ShiftFeat.Create
{
    public class CreateShiftCommandHandler : IRequestHandler<CreateShiftCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateShiftCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(CreateShiftCommand request, CancellationToken cancellationToken)
        {
            var existShift = _unitOfWork.ShiftRepository.Get(filter: s => s.ShiftName.Equals(request.ShiftName) && s.FarmId.Equals(request.FarmId) && s.IsDeleted == false).FirstOrDefault();
            if (existShift != null)
            {
                return BaseResponse<bool>.FailureResponse("Tên ca làm đã tồn tại");
            }

            var shift = _mapper.Map<Shift>(request);
            _unitOfWork.ShiftRepository.Insert(shift);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? BaseResponse<bool>.SuccessResponse("Thêm ca làm thành công")
                : BaseResponse<bool>.FailureResponse("Thêm thất bại");
        }
    }
}
