using AutoMapper;
using CFMS.Application.DTOs.Auth;
using CFMS.Application.DTOs.Category;
using CFMS.Domain.Dictionaries;
using CFMS.Domain.Entities;
using CFMS.Domain.Enums.Roles;
using CFMS.Domain.Enums.Status;
using CFMS.Domain.Enums.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryResponse>();
                //.ForMember(dest => dest.CategoryType, opt => opt.MapFrom(src => GetCategoryName(src.CategoryType)))
                //.ForMember(dest => dest.Status, opt => opt.MapFrom(src => GetCategoryStatus(src.Status)));
            CreateMap<CreateCategoryCommand, Category>();
            //CreateMap<AddSubCateCommand, Category>()
            //    .ForMember(dest => dest.SubCategories, opt => opt.MapFrom(src => new SubCategory()
            //    {
            //        Description = src.Description,
            //        SubCategoryName = src.SubCategoryName,
            //        Status = src.Status,
            //        DataType = src.DataType,
            //        CategoryId = src.CategoryId
            //    }));
        }

        private string GetCategoryName(CategoryType? categoryType)
        {
            if (categoryType.HasValue && CategoryDictionary.CategoryType.TryGetValue((int)categoryType.Value, out string categoryTypeName))
            {
                return categoryTypeName;
            }

            return "Không xác định";
        }

        private string GetCategoryStatus(CategoryStatus? categoryStatus)
        {
            if (categoryStatus.HasValue && CategoryDictionary.CategoryStatus.TryGetValue((int)categoryStatus.Value, out string categoryStatusName))
            {
                return categoryStatusName;
            }

            return "Không xác định";
        }
    }
}
