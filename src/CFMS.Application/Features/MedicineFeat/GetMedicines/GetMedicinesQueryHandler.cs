using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Features.FoodFeat.GetFoods;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.MedicineFeat.GetMedicines
{
    public class GetMedicinesQueryHandler : IRequestHandler<GetMedicinesQuery, BaseResponse<IEnumerable<Medicine>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMedicinesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<Medicine>>> Handle(GetMedicinesQuery request, CancellationToken cancellationToken)
        {
            var medicines = _unitOfWork.FoodRepository.Get(filter: f => f.IsDeleted == false);
            return BaseResponse<IEnumerable<Medicine>>.SuccessResponse(_mapper.Map<IEnumerable<Medicine>>(medicines));
        }
    }
}
