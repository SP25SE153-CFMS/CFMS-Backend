using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.NotiFeat.ReadNoti
{
    public class ReadNotiCommand : IRequest<BaseResponse<bool>>
    {
        public ReadNotiCommand(Guid notificationId)
        {
            NotificationId = notificationId;
        }

        public Guid NotificationId { get; set; }
    }
}
