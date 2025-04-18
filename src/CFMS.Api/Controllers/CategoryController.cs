using CFMS.Application.Features.CategoryFeat.AddSubCate;
using CFMS.Application.Features.CategoryFeat.Create;
using CFMS.Application.Features.CategoryFeat.Delete;
using CFMS.Application.Features.CategoryFeat.GetCategories;
using CFMS.Application.Features.CategoryFeat.GetCategory;
using CFMS.Application.Features.CategoryFeat.GetCategoryByType;
using CFMS.Application.Features.CategoryFeat.GetChickenTypes;
using CFMS.Application.Features.CategoryFeat.GetSubsByType;
using CFMS.Application.Features.CategoryFeat.Update;
using CFMS.Application.Features.CategoryFeat.GetSubsByTypeAndFarm;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CFMS.Application.Features.CategoryFeat.GetSub;

namespace CFMS.Api.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await Send(new GetCategoriesQuery());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Send(new GetCategoryQuery(id));
            return result;
        }

        [HttpGet("categoryId/{id}")]
        public async Task<IActionResult> GetByCategoryId(Guid id)
        {
            var result = await Send(new GetCategoryQuery(id));
            return result;
        }

        [HttpGet("get-sub-cate/{id}")]
        public async Task<IActionResult> GetSub(Guid subCategoryId)
        {
            var result = await Send(new GetSubQuery(subCategoryId));
            return result;
        }

        [HttpGet("categoryType/{type}")]
        public async Task<IActionResult> GetByCategoryByType(string type)
        {
            var result = await Send(new GetCategoryByTypeQuery(type));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCategoryCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Send(new DeleteCategoryCommand(id));
            return result;
        }

        [HttpPost("addSub")]
        public async Task<IActionResult> AddSubCategory(AddSubCateCommand command)
        {
            var result = await Send(command);
            return result;
        }

        [HttpGet("get-chickentypes")]
        public async Task<IActionResult> GetChickenTypes()
        {
            var result = await Send(new GetChickenTypesQuery());
            return result;
        }

        [HttpGet("sub-by-type/{categoryType}")]
        public async Task<IActionResult> GetSubsByType(string categoryType)
        {
            var result = await Send(new GetSubsByTypeQuery(categoryType));
            return result;
        }

        [HttpGet("sub-by-type-and-farm/{categoryType}/{farmId}")]
        public async Task<IActionResult> GetSubsByType(string categoryType, Guid farmId)
        {
            var result = await Send(new GetSubsByTypeAndFarmQuery(categoryType, farmId));
            return result;
        }
    }
}
