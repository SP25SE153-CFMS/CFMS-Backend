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

namespace CFMS.Application.Features.MedicineFeat.Create
{
    public class CreateMedicineCommandHandler : IRequestHandler<CreateMedicineCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateMedicineCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(CreateMedicineCommand request, CancellationToken cancellationToken)
        {
            var existFood = _unitOfWork.MedicineRepository.Get(filter: s => s.MedicineCode.Equals(request.MedicineCode) || s.MedicineName.Equals(request.MedicineName)).FirstOrDefault();
            if (existFood != null)
            {
                return BaseResponse<bool>.FailureResponse("Tên hoặc mã dược phẩm đã tồn tại");
            }

            var medicine = _mapper.Map<Medicine>(request);
            _unitOfWork.MedicineRepository.Insert(medicine);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? BaseResponse<bool>.SuccessResponse("Thêm dược phẩm thành công")
                : BaseResponse<bool>.FailureResponse("Thêm thất bại");
        }
    }
}
