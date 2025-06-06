﻿using AutoMapper;
using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.BreedingAreaFeat.Create
{
    public class CreateBreedingAreaCommandHandler : IRequestHandler<CreateBreedingAreaCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUtilityService _utilityService;

        public CreateBreedingAreaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUtilityService utilityService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _utilityService = utilityService;
        }

        public async Task<BaseResponse<bool>> Handle(CreateBreedingAreaCommand request, CancellationToken cancellationToken)
        {
            var existFarm = _unitOfWork.FarmRepository.Get(filter: f => f.FarmId.Equals(request.FarmId) && !f.IsDeleted, includeProperties: "AreaUnit,BreedingAreas,BreedingAreas.AreaUnit").FirstOrDefault();
            if (existFarm == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Trang trại không tồn tại");
            }

            var existBreedingArea = _unitOfWork.BreedingAreaRepository.Get(filter: ba => ba.BreedingAreaCode.Equals(request.BreedingAreaCode) && ba.FarmId.Equals(request.FarmId) && ba.IsDeleted == false).FirstOrDefault();
            if (existBreedingArea != null)

            {
                return BaseResponse<bool>.FailureResponse(message: "Mã khu nuôi đã tồn tại");
            }

            var existAreaUnit = _unitOfWork.SubCategoryRepository.Get(filter: s => s.SubCategoryId.Equals(request.AreaUnitId) && !s.IsDeleted).FirstOrDefault();
            if (existAreaUnit == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Đơn vị diện tích không tồn tại");
            }

            try
            {
                var breeding = _mapper.Map<BreedingArea>(request);

                decimal farmAreaInM2 = _utilityService.ConvertToSquareMeters(existFarm.Area.Value, existFarm.AreaUnit.SubCategoryName);
                decimal usedAreaInM2 = 0;

                foreach (var ba in existFarm.BreedingAreas)
                {
                    if (ba.Area.HasValue && ba.AreaUnit != null)
                    {
                        usedAreaInM2 += _utilityService.ConvertToSquareMeters(ba.Area.Value, ba.AreaUnit.SubCategoryName);
                    }
                }

                decimal breedingAreaInM2 = _utilityService.ConvertToSquareMeters(breeding.Area.Value, existAreaUnit.SubCategoryName);

                if (breedingAreaInM2 > (farmAreaInM2 - usedAreaInM2))
                {
                    return BaseResponse<bool>.FailureResponse(message: "Diện tích còn lại không đủ để tạo khu nuôi mới");
                }

                _unitOfWork.BreedingAreaRepository.Insert(breeding);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Tạo khu nuôi thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Tạo khu nuôi thất bại");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra: " + ex.Message);
            }

        }
    }
}
