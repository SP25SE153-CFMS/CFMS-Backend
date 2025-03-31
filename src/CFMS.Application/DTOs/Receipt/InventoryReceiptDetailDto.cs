using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.Receipt
{
    public class InventoryReceiptDetailDto
    {
        public decimal ActualQuantity { get; set; }
        public DateTime ActualDate { get; set; }
        public string Note { get; set; }
        public int BatchNumber { get; set; }
    }
}
