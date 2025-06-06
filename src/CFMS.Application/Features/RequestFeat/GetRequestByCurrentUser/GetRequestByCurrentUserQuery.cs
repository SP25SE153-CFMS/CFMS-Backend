﻿using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.RequestFeat.GetRequestByCurrentUser
{
    public class GetRequestByCurrentUserQuery : IRequest<BaseResponse<IEnumerable<Request>>>
    {
    }
}
