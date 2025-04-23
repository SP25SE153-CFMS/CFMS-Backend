using CFMS.Application.Common;
using CFMS.Application.DTOs.Farm;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.FarmFeat.InviteEnrollFarm
{
    public class InviteEnrollFarmCommand : IRequest<BaseResponse<bool>>
    {
        public InviteEnrollFarmCommand(string? farmCode, string methodAccess, int? farmRole, List<FarmEmployeeInvitationRequest>? employessInvitation)
        {
            FarmCode = farmCode;
            MethodAccess = methodAccess;
            FarmRole = farmRole;
            this.employessInvitation = employessInvitation;
        }

        public string? FarmCode { get; set; }
        public string MethodAccess { get; set; }
        public int? FarmRole { get; set; }
        public List<FarmEmployeeInvitationRequest>? employessInvitation { get; set; } = new List<FarmEmployeeInvitationRequest>();
    }
}
