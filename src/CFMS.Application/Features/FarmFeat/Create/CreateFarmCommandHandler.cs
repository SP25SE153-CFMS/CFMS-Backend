using AutoMapper;
using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Commands.FarmFeat.Create
{
    public class CreateFarmCommandHandler : IRequestHandler<CreateFarmCommand, BaseResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFarmCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<string>> Handle(CreateFarmCommand request, CancellationToken cancellationToken)
        {
            var existFarm = _unitOfWork.FarmRepository.Get(filter: f => f.FarmCode.Equals(request.FarmCode));
            if (existFarm != null)
            {
                return BaseResponse<string>.FailureResponse("FarmCode đã tồn tại");
            }

            try
            {
                _unitOfWork.FarmRepository.Insert(_mapper.Map<Farm>(request));
                var result = _unitOfWork.Save();
                if (result > 0)
                {
                    return BaseResponse<string>.SuccessResponse("Tạo Farm thành công");
                }
                return BaseResponse<string>.FailureResponse("Tạo Farm không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<string>.FailureResponse(ex.Message);
            }
        }
    }
}
