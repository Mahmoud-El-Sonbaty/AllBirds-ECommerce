    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.OrderDetailsDTOs
{
    public class ProductColorSizeDTO
    {
        public int Id { get; set; }
        public string? ColorName { get; set; }
        public string ?ProductName { get; set; }
        public int SizeId { get; set; }
        public string ?Size { get; set; }
        public int ProductColorId { get; set; }

    }
}
