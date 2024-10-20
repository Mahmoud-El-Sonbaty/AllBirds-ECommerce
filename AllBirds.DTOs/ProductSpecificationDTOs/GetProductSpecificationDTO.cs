using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.ProductSpecificationDTOs
{
    public class GetProductSpecificationDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductNo { get; set; }
        public string? ProductNameAr { get; set; }
        public string? ProductNameEn { get; set; }
        public string? SpecificationNameAr { get; set; }
        public string? SpecificationNameEn { get; set; }
        public string? Content { get; set; }
    }
}
