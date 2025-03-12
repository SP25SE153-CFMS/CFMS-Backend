using AutoMapper;
using CFMS.Application.DTOs.Auth;
using CFMS.Application.Features.FarmFeat.Create;
using CFMS.Application.Features.UserFeat.GetUsers;
using CFMS.Domain.Dictionaries;
using CFMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.SystemRole, opt => opt.MapFrom(src =>
                    src.SystemRole.HasValue ? src.SystemRole.ToString() : "Không xác định"));
        }
    }
}
