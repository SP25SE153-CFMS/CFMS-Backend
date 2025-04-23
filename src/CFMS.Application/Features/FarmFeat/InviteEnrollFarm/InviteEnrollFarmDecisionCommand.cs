using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.FarmFeat.InviteEnrollFarm
{
    public class InviteEnrollFarmDecisionCommand : IRequest<BaseResponse<bool>>
    {
        public InviteEnrollFarmDecisionCommand(Guid? notificationId, int? decision)
        {
            NotificationId = notificationId;
            Decision = decision;
        }

        public Guid? NotificationId { get; set; }
        public int? Decision { get; set; }
    }
}
