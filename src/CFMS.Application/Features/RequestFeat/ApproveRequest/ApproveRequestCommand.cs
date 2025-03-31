using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.RequestFeat.ApproveRequest
{
    public class ApproveRequestCommand : IRequest<BaseResponse<bool>>
    {
        public Guid RequestId { get; set; }
        public int IsApproved { get; set; }

        public ApproveRequestCommand(int isApproved, Guid requestId)
        {
            IsApproved = isApproved;
            RequestId = requestId;
        }
    }
}
