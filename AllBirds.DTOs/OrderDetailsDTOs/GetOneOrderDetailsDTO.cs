using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.OrderDetailsDTOs
{
    public class GetOneOrderDetailsDTO
    {
        public int Id { get; set; }

        public int OrderMasterId { get; set; }

        public ProductColorSizeDTO ProductColorSize { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int OrderStateId { get; set; }
        public string OrderState { get; set; }

        public int SizeId { get; set; }
        public string size { get; set; }
        public int Quantity { get; set; }
        public string? Notes { get; set; }
    }
}
