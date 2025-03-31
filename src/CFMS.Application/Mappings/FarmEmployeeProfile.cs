using AutoMapper;
using CFMS.Application.DTOs.FarmEmployee;
using CFMS.Domain.Entities;

namespace CFMS.Application.Mappings
{
    public class FarmEmployeeProfile : Profile
    {
        public FarmEmployeeProfile()
        {
            CreateMap<FarmEmployee, FarmEmployeeResponse>();
        }
    }
}
