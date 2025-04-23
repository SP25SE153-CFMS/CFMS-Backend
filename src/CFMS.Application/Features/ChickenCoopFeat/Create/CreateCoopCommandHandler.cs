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
            var existBreedingArea = _unitOfWork.BreedingAreaRepository.Get(filter: ba => ba.BreedingAreaId.Equals(request.BreedingAreaId) && ba.IsDeleted == false).FirstOrDefault();
            if (existBreedingArea == null)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Khu nuôi không tồn tại");
            }

            var existPurpose = _unitOfWork.SubCategoryRepository.Get(filter: s => s.SubCategoryId.Equals(request.PurposeId) && s.IsDeleted == false).FirstOrDefault();
            if (existPurpose == null)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Mục đích nuôi không tồn tại");
            }

            var existCoop = _unitOfWork.ChickenCoopRepository.Get(filter: c => c.ChickenCoopCode.Equals(request.ChickenCoopCode) && c.BreedingAreaId.Equals(request.BreedingAreaId) && c.IsDeleted == false).FirstOrDefault();
            if (existCoop != null)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Mã chuồng gà đã tồn tại");
            }

            try
            {
                var coop = _mapper.Map<ChickenCoop>(request);
                coop.Status = 0;

                _unitOfWork.ChickenCoopRepository.Insert(coop);
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
