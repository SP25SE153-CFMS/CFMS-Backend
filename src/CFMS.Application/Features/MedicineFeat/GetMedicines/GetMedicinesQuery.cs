using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.MedicineFeat.GetMedicines
{
    public class GetMedicinesQuery : IRequest<BaseResponse<IEnumerable<Medicine>>>
    {
        public GetMedicinesQuery() { }
    }
}
