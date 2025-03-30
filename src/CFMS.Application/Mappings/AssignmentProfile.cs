using AutoMapper;
using CFMS.Application.Features.AssignmentFeat.AssignEmployee;
using CFMS.Domain.Entities;

namespace CFMS.Application.Mappings
{
    public class AssignmentProfile : Profile
    {
        public AssignmentProfile()
        {
            CreateMap<AssignEmployeeCommand, Assignment>();
        }
    }
}
