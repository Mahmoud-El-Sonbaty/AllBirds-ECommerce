using AllBirds.DTOs.OrderDetailsDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.OrderMasterDTOs
{
    public class createOrderMasterDTO
    {

        public int Id { get; set; }
        [MaxLength(100)]

        public string OrderNo { get; set; }

        public int ClientId { get; set; }
        [Range(10,15000)]
        public decimal Total { get; set; }
        public int OrderStateId { get; set; }
        [MaxLength(128)]
        public string? Notes { get; set; }
        public int? CouponId { get; set; }
        public List<CreateOrderDetailsDTO> ProductColorSizeId { get; set; }
    }
}
