using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.OrderDetailsDTOs
{
    public class GetAllOrderDetailsDTO
    {
        public int Id { get; set; }

        public int OrderMasterId { get; set; }

        public ProductColorSizeDTO ProductColorSize {  get; set; }
        public int ProductId { get; set; }
        public int OrderStateId { get; set; }

        public int SizeId { get; set; }
        public int Quantity { get; set; }
        public string? Notes { get; set; }

    }
}
