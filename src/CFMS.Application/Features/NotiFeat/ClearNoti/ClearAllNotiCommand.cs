using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.NotiFeat.ClearNoti
{
    public class ClearAllNotiCommand : IRequest<BaseResponse<bool>>
    {
    }
}
