using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.ProductSpecificationDTOs
{
    public class CUProductSpecificationDTO
    {
        public int Id { get; set; }
        public int SpecificationId { get; set; }
        public int ProductId { get; set; }

        [StringLength(1500, MinimumLength = 20)]
        public string? ContentAr { get; set; }

        [StringLength(1500, MinimumLength = 20)]
        public string? ContentEn { get; set; }
    }
}
