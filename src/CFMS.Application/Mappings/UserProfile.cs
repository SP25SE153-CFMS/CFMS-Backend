using AutoMapper;
using CFMS.Application.DTOs.Auth;
using CFMS.Application.Features.FarmFeat.Create;
using CFMS.Application.Features.UserFeat.GetUsers;
using CFMS.Domain.Dictionaries;
using CFMS.Domain.Entities;
using CFMS.Domain.Enums.Roles;
using CFMS.Domain.Enums.Status;
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
            CreateMap<User, UserResponse>();
            //.ForMember(dest => dest.SystemRole, opt => opt.MapFrom(src => GetSystemRoleName(src.SystemRole)))
            //.ForMember(dest => dest.Status, opt => opt.MapFrom(src => GetUserStatusName(src.Status)));
            CreateMap<User, CurrentUserResponse>();
        }

        private string GetSystemRoleName(GeneralRole? systemRole)
        {
            if (systemRole.HasValue && RoleDictionary.SystemRole.TryGetValue((int)systemRole.Value, out string roleName))
            {
                return roleName;
            }

            return "Không xác định";
        }

        private string GetUserStatusName(UserStatus? userStatus)
        {
            if (userStatus.HasValue && StatusDictionary.UserStatus.TryGetValue((int)userStatus.Value, out string userStatusName))
            {
                return userStatusName;
            }

            return "Không xác định";
        }
    }
}
