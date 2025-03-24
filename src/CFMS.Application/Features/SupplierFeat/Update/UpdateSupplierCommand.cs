using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SupplierFeat.Update
{
    public class UpdateSupplierCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateSupplierCommand(string? supplierName, string? supplierCode, string? address, string? phoneNumber, string? bankAccount, int? status, Guid supplierId)
        {
            SupplierName = supplierName;
            SupplierCode = supplierCode;
            Address = address;
            PhoneNumber = phoneNumber;
            BankAccount = bankAccount;
            Status = status;
            SupplierId = supplierId;
        }

        public Guid SupplierId { get; set; }

        public string? SupplierName { get; set; }

        public string? SupplierCode { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public string? BankAccount { get; set; }

        public int? Status { get; set; }
    }
}
