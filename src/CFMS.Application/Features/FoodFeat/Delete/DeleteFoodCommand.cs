using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.FoodFeat.Delete
{
    public class DeleteFoodCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteFoodCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
