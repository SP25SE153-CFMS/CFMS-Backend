using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Services.Impl;
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
        private readonly IUtilityService _utilityService;

        public CreateCoopCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUtilityService utilityService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _utilityService = utilityService;
        }

        public async Task<BaseResponse<bool>> Handle(CreateCoopCommand request, CancellationToken cancellationToken)
        {
            var existBreedingArea = _unitOfWork.BreedingAreaRepository.Get(filter: ba => ba.BreedingAreaId.Equals(request.BreedingAreaId) && ba.IsDeleted == false, includeProperties: "AreaUnit,ChickenCoops,ChickenCoops.AreaUnit").FirstOrDefault();
            if (existBreedingArea == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Khu nuôi không tồn tại");
            }

            var existPurpose = _unitOfWork.SubCategoryRepository.Get(filter: s => s.SubCategoryId.Equals(request.PurposeId) && s.IsDeleted == false).FirstOrDefault();
            if (existPurpose == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Mục đích nuôi không tồn tại");
            }

            var existAreaUnit = _unitOfWork.SubCategoryRepository.Get(filter: s => s.SubCategoryId.Equals(request.AreaUnitId) && !s.IsDeleted).FirstOrDefault();
            if (existAreaUnit == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Đơn vị diện tích không tồn tại");
            }

            var existCoop = _unitOfWork.ChickenCoopRepository.Get(filter: c => c.ChickenCoopCode.Equals(request.ChickenCoopCode) && c.BreedingAreaId.Equals(request.BreedingAreaId) && c.IsDeleted == false).FirstOrDefault();
            if (existCoop != null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Mã chuồng gà đã tồn tại");
            }

            try
            {
                var coop = _mapper.Map<ChickenCoop>(request);

                decimal coopAreaInM2 = _utilityService.ConvertToSquareMeters(coop.Area.Value, existAreaUnit.SubCategoryName);
                decimal breedingAreaInM2 = _utilityService.ConvertToSquareMeters(existBreedingArea.Area.Value, existBreedingArea.AreaUnit.SubCategoryName);

                decimal usedAreaInM2 = 0;
                foreach (var existingCoop in existBreedingArea.ChickenCoops)
                {
                    if (existingCoop.Area.HasValue && existingCoop.AreaUnit != null && !existingCoop.IsDeleted)
                    {
                        usedAreaInM2 += _utilityService.ConvertToSquareMeters(existingCoop.Area.Value, existingCoop.AreaUnit.SubCategoryName);
                    }
                }

                if (coopAreaInM2 > (breedingAreaInM2 - usedAreaInM2))
                {
                    return BaseResponse<bool>.FailureResponse(message: "Diện tích khu nuôi không đủ để tạo chuồng mới");
                }

                coop.Status = 0;
                _unitOfWork.ChickenCoopRepository.Insert(coop);
                var result = await _unitOfWork.SaveChangesAsync();

                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Tạo chuồng thành công");
                }

                return BaseResponse<bool>.FailureResponse(message: "Tạo chuồng không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra: " + ex.Message);
            }
        }
    }
}
