using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.EquipmentFeat.Delete
{
    public class DeleteEquipmentCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteEquipmentCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
