using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.NotiFeat.ClearNoti
{
    public class ClearNotiCommand : IRequest<BaseResponse<bool>>
    {
        public ClearNotiCommand(Guid notificationId)
        {
            NotificationId = notificationId;
        }

        public Guid NotificationId { get; set; }
    }
}
