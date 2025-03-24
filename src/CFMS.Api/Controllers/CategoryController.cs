using CFMS.Application.Features.CategoryFeat.AddSubCate;
using CFMS.Application.Features.CategoryFeat.Create;
using CFMS.Application.Features.CategoryFeat.Delete;
using CFMS.Application.Features.CategoryFeat.GetCategories;
using CFMS.Application.Features.CategoryFeat.GetCategory;
using CFMS.Application.Features.CategoryFeat.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    }
}
