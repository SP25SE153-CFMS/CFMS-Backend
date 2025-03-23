using AutoMapper;
using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.ChickenCoopFeat.Create
{
    public class CreateCoopCommandHandler : IRequestHandler<CreateCoopCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCoopCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(CreateCoopCommand request, CancellationToken cancellationToken)
        {
            var existCoop = _unitOfWork.ChickenCoopRepository.Get(filter: c => c.ChickenCoopCode.Equals(request.ChickenCoopCode) && c.IsDeleted == false).FirstOrDefault();
            if (existCoop != null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Mã chuồng gà đã tồn tại");
            }

            var existBreedingArea = _unitOfWork.BreedingAreaRepository.Get(filter: ba => ba.BreedingAreaId.Equals(request.BreedingAreaId) && ba.IsDeleted == false).FirstOrDefault();
            if (existBreedingArea == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Khu nuôi không tồn tại");
            }

            var existPurpose = _unitOfWork.SubCategoryRepository.Get(filter: s => s.SubCategoryId.Equals(request.PurposeId) && s.IsDeleted == false).FirstOrDefault();
            if (existPurpose == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Mục đích nuôi không tồn tại");
            }

            try
            {
                _unitOfWork.ChickenCoopRepository.Insert(_mapper.Map<ChickenCoop>(request));
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Tạo chuồng thành công");
                }
                return BaseResponse<bool>.SuccessResponse(message: "Tạo chuồng không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
