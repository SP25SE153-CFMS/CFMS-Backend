using CFMS.Application.Common;
using CFMS.Application.DTOs.Chicken;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.CategoryFeat.GetChickenTypes
{
    public class GetChickenTypesQueryHandler : IRequestHandler<GetChickenTypesQuery, BaseResponse<IEnumerable<ChickenTypeGroupDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetChickenTypesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<ChickenTypeGroupDto>>> Handle(GetChickenTypesQuery request, CancellationToken cancellationToken)
        {
            //var subCategories = _unitOfWork.SubCategoryRepository.Get(filter: s => (s.FarmId == null || s.FarmId.Equals(request.FarmId)) && s.Category.CategoryType.Equals("CHICKEN") && s.Status == 1 && s.IsDeleted == false, includeProperties: [s => s.Category, s => s.Chickens]);
            var chickens = _unitOfWork.WareStockRepository
                .Get(filter: ws => ws.Ware.FarmId.Equals(request.FarmId) && ws.Resource.Chicken != null,
                     includeProperties: "Ware,Resource,Resource.Chicken,Resource.Chicken.ChickenType")
                .Select(ws => ws.Resource.Chicken)
                .Where(chicken => chicken != null && chicken.ChickenType != null)
                .GroupBy(chicken => chicken.ChickenType)
                .Select(g => new ChickenTypeGroupDto
                {
                    ChickenType = new ChickenTypeDto
                    {
                        SubCategoryId = g.Key.SubCategoryId,
                        SubCategoryName = g.Key.SubCategoryName,
                        Description = g.Key.Description
                    },
                    Chickens = g.Select(c => new ChickenDto
                    {
                        ChickenId = c.ChickenId,
                        ChickenCode = c.ChickenCode,
                        ChickenName = c.ChickenName,
                        Description = c.Description,
                        Status = c.Status.Value
                    }).ToList()
                });
            return BaseResponse<IEnumerable<ChickenTypeGroupDto>>.SuccessResponse(data: chickens);
        }
    }
}
