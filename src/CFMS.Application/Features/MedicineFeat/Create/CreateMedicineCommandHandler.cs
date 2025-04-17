using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Events;
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
        private readonly IMediator _mediator;

        public CreateMedicineCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<BaseResponse<bool>> Handle(CreateMedicineCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existMedicine = _unitOfWork.MedicineRepository.Get(filter: s => s.MedicineCode.Equals(request.MedicineCode) && s.MedicineName.Equals(request.MedicineName) && s.IsDeleted == false).FirstOrDefault();

                if (existMedicine == null)
                {
                    existMedicine = _mapper.Map<Medicine>(request);
                    _unitOfWork.MedicineRepository.Insert(existMedicine);
                    var result = await _unitOfWork.SaveChangesAsync();
                }

                await _mediator.Publish(new StockUpdatedEvent
                     (
                        existMedicine.MedicineId,
                        0,
                        request.UnitId,
                        "medicine",
                        request.PackageId,
                        request.PackageSize,
                        request.WareId,
                        true
                    ));

                return BaseResponse<bool>.SuccessResponse("Thêm dược phẩm thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse("Thêm thất bại: " + ex.Message);
            }
        }
    }
}
