using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.MedicineFeat.Delete
{
    public class DeleteMedicineCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteMedicineCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
