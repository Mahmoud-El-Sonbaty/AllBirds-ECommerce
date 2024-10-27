using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.ProductDetailDTOs
{
    public class CRProductDetails
    {

        [Required, MaxLength(60)]
        public string? TitleAr { get; set; }

        [Required, MaxLength(60)]
        public string? TitleEn { get; set; }

        [Required , MaxLength(800)]
        public string? DescriptionAr { get; set; }

        [Required ,MaxLength(800)]
        public string? DescriptionEn { get; set; }

        public IFormFile? ImageData { get; set; }

        [MaxLength(1000)]
        public string? ImagePath { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
